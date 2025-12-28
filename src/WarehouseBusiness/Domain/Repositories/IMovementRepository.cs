using Domain.Entities.Weak;
using Domain.Entities.Weak.Movement;
using Domain.Enums;

namespace Domain.Repositories
{
  public interface IMovementRepository : IBaseRepository<MovementDetails>
  {
    Task<List<MovementReport>> GetAllWithDetails(InventoryType? type, SummaryPeriod period, CancellationToken ct);
    Task<List<MovementSummary>> GetSummary(SummaryPeriod period, CancellationToken ct);
  }
}