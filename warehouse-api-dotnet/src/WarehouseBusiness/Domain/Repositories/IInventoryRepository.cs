using Domain.Entities;
using Domain.Entities.Weak;

namespace Domain.Repositories
{
  public interface IInventoryRepository : IBaseRepository<InventoryDetails>
  {
    // Command-side methods (create/post)
    Task InsertAsync(Inventory inv, CancellationToken ct);
    Task<Inventory?> GetRawByIdAsync(string id, CancellationToken ct);
  }
}
