using Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
namespace Infrastructure.DB
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDBConfig> options)
        {
            var config = options.Value;
            var client = new MongoClient(config.ConnectionString);
            _database = client.GetDatabase(config.DatabaseName);
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("users");

    }
}
