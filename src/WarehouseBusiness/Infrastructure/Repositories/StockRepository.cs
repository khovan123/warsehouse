using Domain.Entities;
using Domain.Entities.Weak;
using Domain.Repositories;
using Infrastructure.DB;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly IMongoCollection<Stock> _stocks;
        private readonly IMongoCollection<Product> _products;
        private readonly IMongoCollection<Warehouse> _warehouses;
        private readonly IMongoCollection<Bin> _bins;
        private readonly IMongoCollection<Category> _categories;

        public StockRepository(MongoDbContext context)
        {
            _stocks = context.Stocks;
            _products = context.Products;
            _warehouses = context.Warehouses;
            _bins = context.Bins;
            _categories = context.Categories;
        }

        public async Task<List<Stock>> GetAll(CancellationToken ct)
        {
            var filter = Builders<Stock>.Filter.Empty;
            return await _stocks.Find(filter).ToListAsync(ct);
        }

        public async Task<List<StockReport>> GetAllWithDetails(CancellationToken ct)
        {
            var pipeline = _stocks.Aggregate()
                .Match(s => true)
                .AppendStage<StockReport>(new BsonDocument("$lookup", new BsonDocument
                {
                    { "from", "products" },
                    { "localField", "productId" },
                    { "foreignField", "_id" },
                    { "as", "products" }
                }))
                .AppendStage<StockReport>(new BsonDocument("$lookup", new BsonDocument
                {
                    { "from", "warehouses" },
                    { "localField", "warehouseId" },
                    { "foreignField", "_id" },
                    { "as", "warehouses" }
                }))
                .AppendStage<StockReport>(new BsonDocument("$lookup", new BsonDocument
                {
                    { "from", "bins" },
                    { "localField", "binId" },
                    { "foreignField", "_id" },
                    { "as", "bins" }
                }))
                .AppendStage<StockReport>(new BsonDocument("$lookup", new BsonDocument
                {
                    { "from", "categories" },
                    { "localField", "categoryId" },
                    { "foreignField", "_id" },
                    { "as", "categories" }
                }))
                .AppendStage<StockReport>(new BsonDocument("$addFields", new BsonDocument
                {
                    { "product", new BsonDocument("$arrayElemAt", new BsonArray { "$products", 0 }) },
                    { "warehouse", new BsonDocument("$arrayElemAt", new BsonArray { "$warehouses", 0 }) },
                    { "bin", new BsonDocument("$arrayElemAt", new BsonArray { "$bins", 0 }) },
                    { "category", new BsonDocument("$arrayElemAt", new BsonArray { "$categories", 0 }) }
                }));

            return await pipeline.ToListAsync(ct);
        }
    }
}
