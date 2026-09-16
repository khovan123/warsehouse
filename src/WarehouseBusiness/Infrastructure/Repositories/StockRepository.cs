using Domain.Entities;
using Domain.Entities.Weak;
using Domain.Repositories;
using Infrastructure.Constants;
using Infrastructure.DB;
using Infrastructure.Helpers;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly IMongoCollection<Stock> _stocks;

        public StockRepository(MongoDbContext context)
        {
            _stocks = context.Stocks;
        }

        public async Task<List<StockDetails>?> GetAllAsync(CancellationToken ct)
        {
            var pipeline = new MongoAggregationPipeline<Stock>(_stocks)
                .Match(s => true)
                .Lookup(MongoCollections.Products, "productId", $"tmp_{MongoCollections.Products}")
                .Lookup(MongoCollections.Warehouses, "warehouseId", $"tmp_{MongoCollections.Warehouses}")
                .Lookup(MongoCollections.Bins, "binId", $"tmp_{MongoCollections.Bins}")
                .Lookup(MongoCollections.Categories, "categoryId", $"tmp_{MongoCollections.Categories}")
                .Project(new BsonDocument
                {
                    { "productId", 1 },
                    { "warehouseId", 1 },
                    { "binId", 1 },
                    { "categoryId", 1 },
                    { "onHand", 1 },
                    { "reserved", 1 },
                    { "available", 1 },
                    { "averageCost", 1 },
                    { "inventoryValue", 1 },
                    { "product", BsonDocumentExpression.ArrayElemAt($"tmp_{MongoCollections.Products}", 0) },
                    { "warehouse",BsonDocumentExpression.ArrayElemAt($"tmp_{MongoCollections.Warehouses}", 0) },
                    { "bin", BsonDocumentExpression.ArrayElemAt($"tmp_{MongoCollections.Bins}", 0) },
                    { "category", BsonDocumentExpression.ArrayElemAt($"tmp_{MongoCollections.Categories}", 0) }
                })
                .As<StockDetails>();

            return await pipeline.ToListAsync(ct);
        }

        public async Task<StockDetails?> GetByIdAsync(string id, CancellationToken ct)
        {
            var pipeline = new MongoAggregationPipeline<Stock>(_stocks)
               .Match(s => string.Equals(s.Id, id, StringComparison.OrdinalIgnoreCase))
               .Lookup(MongoCollections.Products, "productId", $"tmp_{MongoCollections.Products}")
               .Lookup(MongoCollections.Warehouses, "warehouseId", $"tmp_{MongoCollections.Warehouses}")
               .Lookup(MongoCollections.Bins, "binId", $"tmp_{MongoCollections.Bins}")
               .Lookup(MongoCollections.Categories, "categoryId", $"tmp_{MongoCollections.Categories}")
               .Project(new BsonDocument
               {
                    { "productId", 1 },
                    { "warehouseId", 1 },
                    { "binId", 1 },
                    { "categoryId", 1 },
                    { "onHand", 1 },
                    { "reserved", 1 },
                    { "available", 1 },
                    { "averageCost", 1 },
                    { "inventoryValue", 1 },
                    { "product", BsonDocumentExpression.ArrayElemAt($"tmp_{MongoCollections.Products}", 0) },
                    { "warehouse",BsonDocumentExpression.ArrayElemAt($"tmp_{MongoCollections.Warehouses}", 0) },
                    { "bin", BsonDocumentExpression.ArrayElemAt($"tmp_{MongoCollections.Bins}", 0) },
                    { "category", BsonDocumentExpression.ArrayElemAt($"tmp_{MongoCollections.Categories}", 0) }
               })
               .As<StockDetails>();

            return await pipeline.FirstOrDefaultAsync(ct);
        }
    }
}
