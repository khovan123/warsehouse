
using Application.Interfaces;

namespace Application.Services
{
  public abstract class BaseService
  {
    private readonly IValidationRunner _validation;

    public BaseService(IValidationRunner validation)
    {
      _validation = validation;
    }

    public async Task DoCheckValidationAsync<TRequest>(TRequest request, CancellationToken ct) => await _validation.RunValidationAsync(request, ct);

  }
}