using Domain.Entities;

namespace Domain.Repositories
{
  public interface ICostingCheckpointRepository
  {
    Task<CostingCheckpoint> GetOrCreateAsync(CancellationToken ct);
    Task UpdateOptimisticAsync(long newLastProcessedSeq, long expectedVersion, CancellationToken ct);
  }
}
