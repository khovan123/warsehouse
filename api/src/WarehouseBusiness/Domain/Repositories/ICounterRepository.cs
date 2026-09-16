namespace Domain.Repositories
{
  public interface ICounterRepository
  {
    Task<long> NextAsync(string counterId, CancellationToken ct);
  }
}