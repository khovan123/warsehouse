using Domain.Entities;
using Domain.Entities.Weak;
using Domain.Enums;

namespace Domain.Repositories
{
  public interface IGoodTransactionRepository : IBaseRepository<GoodTransactionDetails>
  {
    // Command-side methods
    Task InsertAsync(GoodTransaction gt, CancellationToken ct);
    Task<GoodTransaction?> GetRawByIdAsync(string id, CancellationToken ct);
    Task UpdateStatusAsync(string id, FlowStatus status, CancellationToken ct);
    Task UpdateAsync(GoodTransaction gt, CancellationToken ct);
  }
}
