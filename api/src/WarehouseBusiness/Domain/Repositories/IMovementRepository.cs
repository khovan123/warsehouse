using Domain.Entities.Weak;
using Domain.Entities.Weak.Movement;
using Domain.Enums;

namespace Domain.Repositories
{
  public interface IMovementRepository : IBaseRepository<MovementDetails>
  {
    Task<List<MovementReport>> GetAllWithDetailsAsync(InventoryType? type, SummaryPeriod period, CancellationToken ct);
    Task<List<MovementSummary>> GetSummaryAsync(SummaryPeriod period, CancellationToken ct);
    // Command-side methods
    Task InsertAsync(Domain.Entities.Movement mv, CancellationToken ct);
    Task<Domain.Entities.Movement?> GetRawByIdAsync(string id, CancellationToken ct);
    Task UpdateStatusAsync(string id, FlowStatus status, CancellationToken ct);
    Task UpdateAsync(Domain.Entities.Movement mv, CancellationToken ct);
  }
}