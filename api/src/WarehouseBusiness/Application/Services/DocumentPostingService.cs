using Domain.Entities;
using Domain.Enums;
using Infrastructure.Helpers;
using Domain.Repositories;
using Application.Interfaces;

namespace Application.Services
{
  public class DocumentPostingService : IDocumentPostingService
  {
    private readonly ICounterRepository _counter;
    private readonly IInventoryLedgerRepository _ledgerRepo;
    private readonly IStockRepository _stockRepo;
    private readonly IOutboxRepository _outboxRepo;
    private readonly IProductRepository _productRepository;

    public DocumentPostingService(
      ICounterRepository counter,
      IInventoryLedgerRepository ledgerRepo,
      IStockRepository stockRepo,
      IOutboxRepository outboxRepo,
      IProductRepository productRepository)
    {
      _counter = counter;
      _ledgerRepo = ledgerRepo;
      _stockRepo = stockRepo;
      _outboxRepo = outboxRepo;
      _productRepository = productRepository;
    }

    public async Task PostInventoryAsync(Inventory inv, CancellationToken ct)
    {
      var signedQty = inv.Type switch
      {
        InventoryType.Receipt => +inv.Qty,
        InventoryType.Shipment => -inv.Qty,
        _ => inv.Qty
      };

      if (inv.Type == InventoryType.Receipt && inv.UnitCost <= 0)
        throw new InvalidOperationException("Receipt requires UnitCost > 0.");

      var seq = await _counter.NextAsync("inventory_ledger_seq", ct);

      var ledger = new InventoryLedger
      {
        Seq = seq,
        MovementDate = DateHelper.ToUtcDateTime(inv.PostedAt), // bạn đang dùng PostedAt DateOnly :contentReference[oaicite:1]{index=1}
        PostedAt = DateTime.UtcNow,
        SourceType = SourceTypeEnum.INVENTORY,
        SourceId = inv.Id,
        DocNo = inv.Document,
        LineNo = inv.Line,
        ProductId = inv.ProductId,
        WarehouseId = inv.WarehouseId,
        BinId = inv.BinId,
        Uom = inv.Uom,
        Qty = signedQty,
        UnitCostHint = signedQty > 0 ? inv.UnitCost : null,
        InventoryType = inv.Type.ToString()
      };

      await Task.WhenAll(
        _ledgerRepo.InsertAsync(ledger, ct),
        _stockRepo.ApplyOnHandDeltaAsync(inv.ProductId, inv.WarehouseId, inv.BinId, signedQty, ct),
        _outboxRepo.EnqueueCostingAsync("INVENTORY", inv.Id, ct));
    }

    public async Task PostGoodTransactionAsync(GoodTransaction gt, CancellationToken ct)
    {
      if (gt.Status != FlowStatus.COMPLETED && gt.Status != FlowStatus.POSTED)
        throw new InvalidOperationException("GoodTransaction must be Completed/Posted to post.");

      for (int i = 0; i < gt.Lines.Count; i++)
      {
        var line = gt.Lines[i];
        if (line.Difference == 0) continue;

        var seq = await _counter.NextAsync("inventory_ledger_seq", ct);

        var ledger = new InventoryLedger
        {
          Seq = seq,
          MovementDate = gt.PostedAt.ToUtcDateTime(),
          PostedAt = DateTime.UtcNow,
          SourceType = SourceTypeEnum.GOOD_TX,
          SourceId = gt.Id,
          DocNo = gt.DocNo,
          LineNo = i + 1,
          ProductId = line.ProductId,
          WarehouseId = gt.WarehouseId,
          BinId = line.BinId,
          Uom = line.Uom,
          Qty = line.Difference,
          UnitCostHint = null
        };

        await _ledgerRepo.InsertAsync(ledger, ct);

        var product = await _productRepository.GetByIdAsync(line.ProductId, ct);
        await _stockRepo.ApplyOnHandDeltaAsync(line.ProductId, gt.WarehouseId, line.BinId, line.Difference, ct);
      }

      await _outboxRepo.EnqueueCostingAsync("GOOD_TX", gt.Id, ct);
    }

    public async Task PostMovementAsync(
      Movement mv,
      CancellationToken ct)
    {
      if (mv.Status != FlowStatus.COMPLETED && mv.Status != FlowStatus.POSTED)
        throw new InvalidOperationException("Movement must be Completed/Posted to post.");

      for (int i = 0; i < mv.Lines.Count; i++)
      {
        var line = mv.Lines[i];

        // OUT entry
        {
          var seq = await _counter.NextAsync("inventory_ledger_seq", ct);
          var outLedger = new InventoryLedger
          {
            Seq = seq,
            MovementDate = mv.PostedAt.ToUtcDateTime(),
            PostedAt = DateTime.UtcNow,
            SourceType = SourceTypeEnum.MOVEMENT,
            SourceId = mv.Id,
            DocNo = mv.DocNo,
            LineNo = i + 1,
            ProductId = line.ProductId,
            WarehouseId = mv.FromWarehouse,
            BinId = line.FromBin,
            Uom = line.Uom,
            Qty = -line.Qty
          };

          await _ledgerRepo.InsertAsync(outLedger, ct);
          await _stockRepo.ApplyOnHandDeltaAsync(line.ProductId, mv.FromWarehouse, line.FromBin, -line.Qty, ct);
        }

        // IN entry
        {
          var seq = await _counter.NextAsync("inventory_ledger_seq", ct);
          var inLedger = new InventoryLedger
          {
            Seq = seq,
            MovementDate = mv.PostedAt.ToUtcDateTime(),
            PostedAt = DateTime.UtcNow,
            SourceType = SourceTypeEnum.MOVEMENT,
            SourceId = mv.Id,
            DocNo = mv.DocNo,
            LineNo = i + 1,
            ProductId = line.ProductId,
            WarehouseId = mv.ToWarehouse,
            BinId = line.ToBin,
            Uom = line.Uom,
            Qty = +line.Qty
          };

          await _ledgerRepo.InsertAsync(inLedger, ct);
          await _stockRepo.ApplyOnHandDeltaAsync(line.ProductId, mv.ToWarehouse, line.ToBin, +line.Qty, ct);
        }
      }

      await _outboxRepo.EnqueueCostingAsync("MOVEMENT", mv.Id, ct);
    }
  }
}
