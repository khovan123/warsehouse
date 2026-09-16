using Domain.Entities;

namespace Domain.Repositories
{
  public interface IValuationRepository
  {
    Task<bool> ExistsByLedgerIdAsync(string ledgerId, CancellationToken ct);
    Task InsertAsync(ValuationEntry val, CancellationToken ct);
    Task<ValuationEntry?> GetMovementOutValuationAsync(string docNo, int lineNo, string productId, CancellationToken ct);


  }
}