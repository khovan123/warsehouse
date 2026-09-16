using MongoDB.Driver;
using Domain.Entities;
using Infrastructure.DB;
using Domain.Repositories;

namespace Infrastructure.Repositories
{
  public class InventoryLedgerRepository : IInventoryLedgerRepository
  {
    private readonly IMongoCollection<InventoryLedger> _inventoryLedger;
    public InventoryLedgerRepository(MongoDbContext ctx)
    {
      _inventoryLedger = ctx.InventoryLedgers;
    }

    public async Task InsertAsync(InventoryLedger entry, CancellationToken ct)
      => await _inventoryLedger.InsertOneAsync(entry, cancellationToken: ct);

    public async Task<List<InventoryLedger>> FindAfterSeqAsync(long fromSeqExclusive, int limit, CancellationToken ct)
    {
      return await _inventoryLedger
        .Find(x => x.Seq > fromSeqExclusive)
        .SortBy(x => x.Seq)
        .Limit(limit)
        .ToListAsync(ct);
    }
  }
}
