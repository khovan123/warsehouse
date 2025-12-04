using Mapster;

namespace Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        var assembly = typeof(ServiceCollectionExtensions).Assembly;

        // Register FluentValidation validators
        services.AddValidatorsFromAssembly(assembly);

        // Register Mapster mapper
        AddMapsterServices(services);



        return services;
    }

    public static IServiceCollection AddMapsterServices(this IServiceCollection services)
    {
        var assembly = typeof(ServiceCollectionExtensions).Assembly;

        var config = new TypeAdapterConfig();

        config.Scan(assembly);

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}

