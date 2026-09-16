using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;

namespace Application.Services
{
  public class CostingProcessorService : ICostingProcessorService
  {
    private readonly IInventoryLedgerRepository _ledgerRepo;
    private readonly ICostStateRepository _costRepo;
    private readonly IValuationRepository _valRepo;
    private readonly ITransferLinkRepository _transferRepo;
    private readonly IStockRepository _stockRepo;
    private readonly IAccountingService _acct;

    public CostingProcessorService(
      IInventoryLedgerRepository ledgerRepo,
      ICostStateRepository costRepo,
      IValuationRepository valRepo,
      ITransferLinkRepository transferRepo,
      IStockRepository stockRepo,
      IAccountingService acct)
    {
      _ledgerRepo = ledgerRepo;
      _costRepo = costRepo;
      _valRepo = valRepo;
      _transferRepo = transferRepo;
      _stockRepo = stockRepo;
      _acct = acct;
    }

    public async Task ProcessBatchAsync(long fromSeqExclusive, int batchSize, CancellationToken ct)
    {
      var entries = await _ledgerRepo.FindAfterSeqAsync(fromSeqExclusive, batchSize, ct);

      foreach (var e in entries)
      {
        // idempotent by valuation unique ledgerId
        if (await _valRepo.ExistsByLedgerIdAsync(e.Id, ct))
          continue;

        // scope: product+warehouse
        var state = await _costRepo.GetOrCreateAsync(e.ProductId, e.WarehouseId, ct);
        var expectedVersion = state.Version;

        // determine unit cost used
        var unitCostUsed = await ResolveUnitCostAsync(state, e, ct);

        // compute amount
        var amount = unitCostUsed * e.Qty;

        // apply to cost state
        ApplyAvg(state, e.Seq, e.Qty, unitCostUsed);

        // write valuation
        var val = new ValuationEntry
        {
          LedgerId = e.Id,
          Seq = e.Seq,
          ProductId = e.ProductId,
          WarehouseId = e.WarehouseId,
          BinId = e.BinId,
          Qty = e.Qty,
          UnitCost = unitCostUsed,
          Amount = amount,
          PostingDate = e.MovementDate,
          SourceType = e.SourceType,
          DocNo = e.DocNo,
          LineNo = e.LineNo
        };

        // persist (đơn giản: retry nếu conflict)
        await PersistWithRetryAsync(async () =>
        {
          await _valRepo.InsertAsync(val, ct);

          // movement OUT -> save transfer unit cost for later IN
          if (e.SourceType == SourceTypeEnum.MOVEMENT && e.Qty < 0)
            await _transferRepo.UpsertAsync(e.DocNo, e.LineNo, e.ProductId, unitCostUsed, e.Id, ct);

          await _costRepo.UpdateOptimisticAsync(state, expectedVersion, ct);

          // accounting
          await _acct.PostFromValuationAsync(val, ct);

          // optional: update Stock projection avg/value cho bin vừa phát sinh
          var stock = await _stockRepo.GetAsync(e.ProductId, e.WarehouseId, e.BinId, ct);
          if (stock != null)
            await _stockRepo.UpdateCostProjectionAsync(e.ProductId, e.WarehouseId, e.BinId, state.AverageCost, stock.OnHand, ct);

        }, maxRetry: 3, ct);
      }
    }

    private async Task<decimal> ResolveUnitCostAsync(CostState state, InventoryLedger e, CancellationToken ct)
    {
      // Movement IN: lấy unitCost từ link/valuation OUT để đảm bảo value đi theo hàng
      if (e.SourceType == SourceTypeEnum.MOVEMENT && e.Qty > 0)
      {
        var link = await _transferRepo.GetAsync(e.DocNo, e.LineNo, e.ProductId, ct);
        if (link != null) return link.UnitCost;

        var outVal = await _valRepo.GetMovementOutValuationAsync(e.DocNo, e.LineNo, e.ProductId, ct);
        if (outVal != null) return outVal.UnitCost;

        // fallback
        return state.OnHandQty > 0 ? state.AverageCost : 0m;
      }

      // Inbound (receipt/adjust+)
      if (e.Qty > 0)
      {
        if (e.UnitCostHint.HasValue && e.UnitCostHint.Value > 0) return e.UnitCostHint.Value;
        return state.OnHandQty > 0 ? state.AverageCost : 0m;
      }

      // Outbound (issue/adjust-)
      return state.OnHandQty > 0 ? state.AverageCost : 0m;
    }

    private static void ApplyAvg(CostState s, long seq, int signedQty, decimal unitCostUsed)
    {
      if (signedQty > 0)
      {
        s.OnHandQty += signedQty;
        s.OnHandValue += unitCostUsed * signedQty;
      }
      else if (signedQty < 0)
      {
        var issueQty = -signedQty;
        var avg = s.OnHandQty > 0 ? s.AverageCost : 0m;
        var valueOut = avg * issueQty;

        s.OnHandQty -= issueQty;
        s.OnHandValue -= valueOut;

        if (s.OnHandQty <= 0)
        {
          s.OnHandQty = Math.Max(0, s.OnHandQty);
          if (s.OnHandQty == 0) s.OnHandValue = 0m;
        }
      }

      s.AverageCost = s.OnHandQty > 0 ? (s.OnHandValue / s.OnHandQty) : 0m;
      s.LastSeq = seq;
      s.UpdatedAt = DateTime.UtcNow;
      // Version tăng trong UpdateOptimisticAsync (Inc)
    }

    private static async Task PersistWithRetryAsync(Func<Task> action, int maxRetry, CancellationToken ct)
    {
      for (int i = 1; i <= maxRetry; i++)
      {
        try
        {
          await action();
          return;
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Optimistic concurrency") && i < maxRetry)
        {
          // retry
        }
        ct.ThrowIfCancellationRequested();
      }
      // cuối cùng nếu vẫn fail thì throw (để scheduler retry batch)
      await action();
    }

    public async Task<CostingBatchResult> ProcessBatchWithResultAsync(long fromSeqExclusive, int batchSize, CancellationToken ct)
    {
      var entries = await _ledgerRepo.FindAfterSeqAsync(fromSeqExclusive, batchSize, ct);

      long maxSeq = fromSeqExclusive;
      int processed = 0;

      foreach (var e in entries)
      {
        maxSeq = Math.Max(maxSeq, e.Seq);

        if (await _valRepo.ExistsByLedgerIdAsync(e.Id, ct))
        {
          // đã xử lý rồi (idempotent), vẫn tính là "đã đi qua" seq đó
          processed++;
          continue;
        }

        var state = await _costRepo.GetOrCreateAsync(e.ProductId, e.WarehouseId, ct);
        var expectedVersion = state.Version;

        var unitCostUsed = await ResolveUnitCostAsync(state, e, ct);
        var amount = unitCostUsed * e.Qty;

        ApplyAvg(state, e.Seq, e.Qty, unitCostUsed);

        var val = new ValuationEntry
        {
          LedgerId = e.Id,
          Seq = e.Seq,
          ProductId = e.ProductId,
          WarehouseId = e.WarehouseId,
          BinId = e.BinId,
          Qty = e.Qty,
          UnitCost = unitCostUsed,
          Amount = amount,
          PostingDate = e.MovementDate,
          SourceType = e.SourceType,
          DocNo = e.DocNo,
          LineNo = e.LineNo
        };

        await PersistWithRetryAsync(async () =>
        {
          await _valRepo.InsertAsync(val, ct);

          if (e.SourceType == SourceTypeEnum.MOVEMENT && e.Qty < 0)
            await _transferRepo.UpsertAsync(e.DocNo, e.LineNo, e.ProductId, unitCostUsed, e.Id, ct);

          await _costRepo.UpdateOptimisticAsync(state, expectedVersion, ct);

          await _acct.PostFromValuationAsync(val, ct);

          // optional stock projection
          var stock = await _stockRepo.GetAsync(e.ProductId, e.WarehouseId, e.BinId, ct);
          if (stock != null)
            await _stockRepo.UpdateCostProjectionAsync(e.ProductId, e.WarehouseId, e.BinId, state.AverageCost, stock.OnHand, ct);

        }, maxRetry: 3, ct);

        processed++;
      }

      // Nếu entries rỗng => processed=0, maxSeq vẫn là fromSeqExclusive
      return processed == 0
        ? new CostingBatchResult(0, fromSeqExclusive)
        : new CostingBatchResult(processed, maxSeq);
    }

  }
}
