using Application.Interfaces;
using Contract.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services
{
  public sealed class ValidationRunner : IValidationRunner
  {
    private readonly IServiceProvider _sp;

    public ValidationRunner(IServiceProvider sp)
    {
      _sp = sp;
    }

    public async Task RunValidationAsync<TRequest>(TRequest request, CancellationToken ct)
    {
      if (request is null) return;

      if (request is not IFlagValidatableRequest) return;

      var validators = _sp.GetServices<IValidator<TRequest>>()?.ToList() ?? new List<IValidator<TRequest>>();
      if (validators.Count == 0) return;

      var failures = new List<ValidationFailure>();

      foreach (var v in validators)
      {
        var result = await v.ValidateAsync(request, ct);
        if (!result.IsValid)
          failures.AddRange(result.Errors);
      }

      if (failures.Count > 0)
        throw new ValidationException(failures);
    }
  }
}
