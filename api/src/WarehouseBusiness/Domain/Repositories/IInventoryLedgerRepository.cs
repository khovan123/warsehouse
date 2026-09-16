using Domain.Entities;

namespace Domain.Repositories
{
  public interface IInventoryLedgerRepository
  {
    Task InsertAsync(InventoryLedger entry, CancellationToken ct);
    Task<List<InventoryLedger>> FindAfterSeqAsync(long fromSeqExclusive, int limit, CancellationToken ct);
  }
}
