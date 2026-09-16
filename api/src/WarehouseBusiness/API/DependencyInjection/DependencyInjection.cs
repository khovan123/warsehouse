using System.Reflection;
using Application.DependencyInjection;
using Application.Helper.Options;
using Application.Services;
using Application.Workers;
using Infrastructure.Repositories;
using MongoDB.Driver;

namespace API.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProjectDependencies(this IServiceCollection services)
        {
            services.AddApplicationDependencies();
            services.AddApplicationServices();
            services.AddInfrastructureRepositories();
            services.Configure<CostingWorkerOptions>(opt =>
            {
                opt.BatchSize = 200;
                opt.PollDelayMs = 500;
                opt.ErrorDelayMs = 2000;
            });
            services.AddHostedService<CostingBackgroundService>(); return services;
        }

        private static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var applicationAssembly = typeof(HealthService).Assembly;

            services.RegisterByConvention(
                assembly: applicationAssembly,
                classNameSuffix: "Service",
                lifetime: ServiceLifetime.Scoped);

            return services;
        }

        private static IServiceCollection AddInfrastructureRepositories(this IServiceCollection services)
        {
            var infrastructureAssembly = typeof(UserRepository).Assembly;

            services.RegisterByConvention(
                assembly: infrastructureAssembly,
                classNameSuffix: "Repository",
                lifetime: ServiceLifetime.Scoped);

            return services;
        }

        private static IServiceCollection RegisterByConvention(
            this IServiceCollection services,
            Assembly assembly,
            string classNameSuffix,
            ServiceLifetime lifetime)
        {
            var implTypes = assembly
                .GetTypes()
                .Where(t =>
                    t.IsClass &&
                    !t.IsAbstract &&
                    t.Name.EndsWith(classNameSuffix, StringComparison.Ordinal));

            foreach (var implType in implTypes)
            {
                var expectedInterfaceName = "I" + implType.Name;

                var serviceType = implType
                    .GetInterfaces()
                    .FirstOrDefault(i => i.Name.Equals(expectedInterfaceName, StringComparison.Ordinal));

                if (serviceType is null)
                {
                    continue;
                }

                services.Add(new ServiceDescriptor(serviceType, implType, lifetime));
            }

            return services;
        }
    }
}
