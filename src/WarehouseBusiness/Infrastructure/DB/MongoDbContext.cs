using Domain.Entities;
using Infrastructure.Constants;
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

        public IMongoCollection<User> Users => _database.GetCollection<User>(MongoCollections.Users);

        public IMongoCollection<Category> Categories => _database.GetCollection<Category>(MongoCollections.Categories);

        public IMongoCollection<Warehouse> Warehouses => _database.GetCollection<Warehouse>(MongoCollections.Warehouses);

        public IMongoCollection<Bin> Bins => _database.GetCollection<Bin>(MongoCollections.Bins);

        public IMongoCollection<Product> Products => _database.GetCollection<Product>(MongoCollections.Products);

        public IMongoCollection<BusinessPartner> BusinessPartnets => _database.GetCollection<BusinessPartner>(MongoCollections.BusinessPartners);

        public IMongoCollection<Inventory> Inventories => _database.GetCollection<Inventory>(MongoCollections.Inventory);
    }
}
