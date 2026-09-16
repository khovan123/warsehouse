using Domain.Entities;

namespace Domain.Repositories
{
  public interface ITransferLinkRepository
  {
    Task<TransferCostLink?> GetAsync(string docNo, int lineNo, string productId, CancellationToken ct);
    Task UpsertAsync(string docNo, int lineNo, string productId, decimal unitCost, string outLedgerId, CancellationToken ct);

  }
}