using Domain.Entities;

namespace Application.Interfaces
{
  public interface IAccountingService
  {
    Task PostFromValuationAsync(ValuationEntry val, CancellationToken ct);
  }
}