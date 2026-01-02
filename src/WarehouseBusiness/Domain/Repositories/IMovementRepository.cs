using Domain.Entities.Weak;
using Domain.Entities.Weak.Movement;
using Domain.Enums;

namespace Domain.Repositories
{
  public interface IMovementRepository : IBaseRepository<MovementDetails>
  {

    Task<List<MovementReport>> GetAllWithDetailsAsync(InventoryType? type, SummaryPeriod period, CancellationToken ct);
    Task<List<MovementSummary>> GetSummaryAsync(SummaryPeriod period, CancellationToken ct);
    Task<List<MaterialTransaction>> GetMaterialTransactionAsync(InventoryType? type, string? warehouseId, CancellationToken ct);
  }
}