using Domain.Entities;
using MongoDB.Driver;

namespace Infrastructure.DB
{
  public static class MongoIndexInitializer
  {
    public static async Task EnsureIndexesAsync(MongoDbContext ctx, CancellationToken ct)
    {
      var tasks = new List<Task>
      {
          // inventory_ledger
          ctx.InventoryLedgers.Indexes.CreateManyAsync(new[]
      {
        new CreateIndexModel<InventoryLedger>(
          Builders<InventoryLedger>.IndexKeys.Ascending(x => x.Seq),
          new CreateIndexOptions { Unique = true, Name = "uq_inventory_ledger_seq" }),

        new CreateIndexModel<InventoryLedger>(
          Builders<InventoryLedger>.IndexKeys
            .Ascending(x => x.ProductId)
            .Ascending(x => x.WarehouseId)
            .Ascending(x => x.MovementDate)
            .Ascending(x => x.Seq),
          new CreateIndexOptions { Name = "ix_inventory_ledger_dim_date_seq" }),

        // idempotency for posting a line to ledger
        new CreateIndexModel<InventoryLedger>(
          Builders<InventoryLedger>.IndexKeys
            .Ascending(x => x.SourceType)
            .Ascending(x => x.SourceId)
            .Ascending(x => x.LineNo)
            .Ascending(x => x.BinId)
            .Ascending(x => x.Qty),
          new CreateIndexOptions { Unique = true, Name = "uq_inventory_ledger_source" })
      }, cancellationToken: ct),

          // cost_state
          ctx.CostStates.Indexes.CreateOneAsync(
        new CreateIndexModel<CostState>(
          Builders<CostState>.IndexKeys
            .Ascending(x => x.ProductId)
            .Ascending(x => x.WarehouseId),
          new CreateIndexOptions { Unique = true, Name = "uq_cost_state_scope" }),
        cancellationToken: ct),

          // valuation_entry
          ctx.Valuations.Indexes.CreateOneAsync(
        new CreateIndexModel<ValuationEntry>(
          Builders<ValuationEntry>.IndexKeys.Ascending(x => x.LedgerId),
          new CreateIndexOptions { Unique = true, Name = "uq_valuation_ledger" }),
        cancellationToken: ct),

          // accounting_journal
          ctx.Journals.Indexes.CreateOneAsync(
        new CreateIndexModel<AccountingJournal>(
          Builders<AccountingJournal>.IndexKeys.Ascending(x => x.SourceValuationId),
          new CreateIndexOptions { Unique = true, Name = "uq_journal_valuation" }),
        cancellationToken: ct),

          // transfer_cost_link
          ctx.TransferLinks.Indexes.CreateOneAsync(
        new CreateIndexModel<TransferCostLink>(
          Builders<TransferCostLink>.IndexKeys
            .Ascending(x => x.DocNo)
            .Ascending(x => x.LineNo)
            .Ascending(x => x.ProductId),
          new CreateIndexOptions { Unique = true, Name = "uq_transfer_doc_line_product" }),
        cancellationToken: ct),

          // outbox
          ctx.Outbox.Indexes.CreateOneAsync(
        new CreateIndexModel<OutboxEvent>(
          Builders<OutboxEvent>.IndexKeys
            .Ascending(x => x.Status)
            .Ascending(x => x.CreatedAt),
          new CreateIndexOptions { Name = "ix_outbox_status_createdAt" }),
        cancellationToken: ct),

          // stocks
        //   ctx.Stocks.Indexes.CreateOneAsync(
        // new CreateIndexModel<Stock>(
        //   Builders<Stock>.IndexKeys
        //     .Ascending(x => x.ProductId)
        //     .Ascending(x => x.WarehouseId)
        //     .Ascending(x => x.BinId),
        //   new CreateIndexOptions { Unique = true, Name = "uq_stock_scope" }),
        // cancellationToken: ct)
      };

      await Task.WhenAll(tasks);
    }
  }
}
