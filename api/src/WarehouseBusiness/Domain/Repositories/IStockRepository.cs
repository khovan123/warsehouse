using Domain.Entities.Weak;

namespace Domain.Repositories
{
  public interface IStockRepository : IBaseRepository<StockDetails>
  {
    // Realtime inventory updates (sync posting path)
    Task ApplyOnHandDeltaAsync(string productId, string warehouseId, string binId, int delta, CancellationToken ct);
    Task ApplyReservedDeltaAsync(string productId, string warehouseId, string binId, int delta, CancellationToken ct);

    // Projection update (async costing path)
    Task UpdateCostProjectionAsync(string productId, string warehouseId, string binId, decimal avgCost, int onHandQty, CancellationToken ct);

    // Read stock by dimension (used by costing worker)
    Task<StockDetails?> GetAsync(string productId, string warehouseId, string binId, CancellationToken ct);
  }
}
