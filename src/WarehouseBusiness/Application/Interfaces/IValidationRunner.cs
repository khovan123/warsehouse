namespace Application.Interfaces
{
  public interface IValidationRunner
  {
    Task RunValidationAsync<TRequest>(TRequest request, CancellationToken ct);
  }
}