using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces;
using Application.Services;
using FluentValidation;

namespace Application.DependencyInjection
{
  public static class DependencyInjection
  {
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
      services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

      services.AddScoped<IValidationRunner, ValidationRunner>();

      return services;
    }
  }
}
