using Infrastructure.DB;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using StackExchange.Redis;

namespace API.Extensions
{
    public static class DatabaseExtentions
    {
        public static IServiceCollection AddMongoDBRunner(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDBConfig>(configuration.GetSection("MONGO"));

            services.AddSingleton<IMongoClient>(sp =>
            {
                var config = sp.GetRequiredService<IOptions<MongoDBConfig>>().Value;
                return new MongoClient(config.ConnectionString);
            });

            services.AddSingleton<IMongoDatabase>(sp =>
            {
                var config = sp.GetRequiredService<IOptions<MongoDBConfig>>().Value;
                var client = sp.GetRequiredService<IMongoClient>();
                return client.GetDatabase(config.DatabaseName);
            });

            services.AddSingleton<MongoDbContext>();

            return services;
        }

        public static IServiceCollection AddPersistKeysToRedis(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
        {
            var redisSection = configuration.GetSection("Redis");
            var user = redisSection.GetValue<string>("User");
            var connectionString = redisSection.GetValue<string>("ConnectionString");
            var port = redisSection.GetValue<int>("Port");
            var password = redisSection.GetValue<string>("Password");
            var applicationName = configuration.GetValue<string>("ApplicationName") ?? "DefaultAppName";
            // var certSection = configuration.GetSection("DataProtection");
            // var certPath = certSection.GetValue<string>("CertificatePath");
            // var certPassword = certSection.GetValue<string>("CertificatePassword");
            // var cert = X509CertificateLoader.LoadPkcs12FromFile(certPath ?? "<URL>.pfx", certPassword);

            var options = new ConfigurationOptions
            {
                EndPoints = { { connectionString ?? "<URL>", port } },
                User = user,
                Password = password,
            };

            services.Configure<RedisConfig>(redisSection);

            var redis = ConnectionMultiplexer.Connect(options);

            services.AddSingleton<IConnectionMultiplexer>(redis);

            services.AddSingleton<IDatabase>(redis.GetDatabase());

            services.AddSingleton<RedisContext>();

            services.AddDataProtection()
                    .SetApplicationName(applicationName)
                    .PersistKeysToStackExchangeRedis(redis, configuration.GetValue<string>("PrefixDataProtectionKey"));
            // .ProtectKeysWithCertificate(cert);

            return services;
        }
    }
}
