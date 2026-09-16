using Domain.Entities;

namespace Domain.Repositories
{
  public interface ICostStateRepository
  {
    Task<CostState> GetOrCreateAsync(string productId, string warehouseId, CancellationToken ct);
    Task UpdateOptimisticAsync(CostState state, long expectedVersion, CancellationToken ct);

  }
}