using Domain.Entities;
using Infrastructure.Constants;
using MongoDB.Driver;
namespace Infrastructure.DB
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IMongoDatabase mongoDatabase)
        {
            _database = mongoDatabase;
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>(MongoCollections.Users);

        public IMongoCollection<Category> Categories => _database.GetCollection<Category>(MongoCollections.Categories);

        public IMongoCollection<Warehouse> Warehouses => _database.GetCollection<Warehouse>(MongoCollections.Warehouses);

        public IMongoCollection<Bin> Bins => _database.GetCollection<Bin>(MongoCollections.Bins);

        public IMongoCollection<Product> Products => _database.GetCollection<Product>(MongoCollections.Products);

        public IMongoCollection<BusinessPartner> BusinessPartnets => _database.GetCollection<BusinessPartner>(MongoCollections.BusinessPartners);

        public IMongoCollection<Inventory> Inventories => _database.GetCollection<Inventory>(MongoCollections.Inventory);
        public IMongoCollection<RefreshToken> RefreshTokens => _database.GetCollection<RefreshToken>(MongoCollections.RefreshTokens);
    }
}
