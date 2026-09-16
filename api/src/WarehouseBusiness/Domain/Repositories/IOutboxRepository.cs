namespace Domain.Repositories
{
  public interface IOutboxRepository
  {
    Task EnqueueCostingAsync(string sourceType, string sourceId, CancellationToken ct);
  }
}