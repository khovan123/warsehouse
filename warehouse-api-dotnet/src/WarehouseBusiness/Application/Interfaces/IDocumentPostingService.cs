using Domain.Entities;

namespace Application.Interfaces
{
  public interface IDocumentPostingService
  {
    Task PostInventoryAsync(Inventory inv, CancellationToken ct);
    Task PostGoodTransactionAsync(GoodTransaction gt, CancellationToken ct);
    Task PostMovementAsync(Movement mv, CancellationToken ct);
  }
}