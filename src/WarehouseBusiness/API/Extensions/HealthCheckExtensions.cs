using Infrastructure.DB;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace API.Extensions
{
  public static class HeathCheckExtensions
  {
    public static IServiceCollection AddMongoDbHealthCheck(this IServiceCollection services)
    {
      services.AddHealthChecks().AddMongoDb(
               clientFactory: sp => sp.GetRequiredService<IMongoClient>(),
               databaseNameFactory: sp =>
               {
                 var config = sp.GetRequiredService<IOptions<MongoDBConfig>>().Value;
                 return config.DatabaseName;
               },
               name: "mongodb"
           );
      return services;
    }
  }
}