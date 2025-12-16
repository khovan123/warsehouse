namespace Application.Interfaces
{
  public interface IValidationRunner
  {
    Task RunValidation<TRequest>(TRequest request, CancellationToken ct);
  }
}