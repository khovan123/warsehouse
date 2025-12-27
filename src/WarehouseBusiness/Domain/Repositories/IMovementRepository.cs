using Domain.Entities;
using Domain.Entities.Weak.Movement;
using Domain.Enums;

namespace Domain.Repositories
{
  public interface IMovementRepository : IBaseRepository<Movement>
  {
    Task<List<MovementReport>> GetAllWithDetails(InventoryType? type, CancellationToken ct);
    Task<List<MovementSummary>> GetSummary(SummaryPeriod period, CancellationToken ct);
  }
}