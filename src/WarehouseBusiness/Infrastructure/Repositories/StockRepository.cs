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
                .Project(new BsonDocument
                {
                    { "product", BsonDocumentExpression.ArrayElemAt($"tmp_{MongoCollections.Products}", 0) },
                    { "warehouse",BsonDocumentExpression.ArrayElemAt($"tmp_{MongoCollections.Warehouses}", 0) },
                    { "bin", BsonDocumentExpression.ArrayElemAt($"tmp_{MongoCollections.Bins}", 0) },
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
               .Project(new BsonDocument
               {
                    { "product", BsonDocumentExpression.ArrayElemAt($"tmp_{MongoCollections.Products}", 0) },
                    { "warehouse",BsonDocumentExpression.ArrayElemAt($"tmp_{MongoCollections.Warehouses}", 0) },
                    { "bin", BsonDocumentExpression.ArrayElemAt($"tmp_{MongoCollections.Bins}", 0) },
               })
               .As<StockDetails>();

            return await pipeline.FirstOrDefaultAsync(ct);
        }

        public async Task ApplyOnHandDeltaAsync(string productId, string warehouseId, string binId, int delta, CancellationToken ct)
        {
            var filter = Builders<Stock>.Filter.Where(x =>
              x.ProductId == productId && x.WarehouseId == warehouseId && x.BinId == binId);

            var update = Builders<Stock>.Update
              .SetOnInsert(x => x.ProductId, productId)
              .SetOnInsert(x => x.WarehouseId, warehouseId)
              .SetOnInsert(x => x.BinId, binId)
              .Inc(x => x.OnHand, delta);

            await _stocks.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true }, ct);
        }

        public async Task ApplyReservedDeltaAsync(string productId, string warehouseId, string binId, int delta, CancellationToken ct)
        {
            var filter = Builders<Stock>.Filter.Where(x =>
              x.ProductId == productId && x.WarehouseId == warehouseId && x.BinId == binId);

            var update = Builders<Stock>.Update
              .SetOnInsert(x => x.ProductId, productId)
              .SetOnInsert(x => x.WarehouseId, warehouseId)
              .SetOnInsert(x => x.BinId, binId)
              .Inc(x => x.Reserved, delta);

            await _stocks.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true }, ct);
        }

        public async Task UpdateCostProjectionAsync(string productId, string warehouseId, string binId, decimal avgCost, int onHandQty, CancellationToken ct)
        {
            var filter = Builders<Stock>.Filter.Where(x =>
              x.ProductId == productId && x.WarehouseId == warehouseId && x.BinId == binId);

            var update = Builders<Stock>.Update
              .Set(x => x.AverageCost, avgCost)
              .Set(x => x.InventoryValue, avgCost * onHandQty);

            await _stocks.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = false }, ct);
        }

        public async Task<StockDetails?> GetAsync(string productId, string warehouseId, string binId, CancellationToken ct)
        {
            var pipeline = new MongoAggregationPipeline<Stock>(_stocks)
              .Match(s =>
                    string.Equals(s.ProductId, productId, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(s.WarehouseId, warehouseId, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(s.BinId, binId, StringComparison.OrdinalIgnoreCase)
              )
              .Lookup(MongoCollections.Products, "productId", $"tmp_{MongoCollections.Products}")
              .Lookup(MongoCollections.Warehouses, "warehouseId", $"tmp_{MongoCollections.Warehouses}")
              .Lookup(MongoCollections.Bins, "binId", $"tmp_{MongoCollections.Bins}")
              .Project(new BsonDocument
              {
                    { "product", BsonDocumentExpression.ArrayElemAt($"tmp_{MongoCollections.Products}", 0) },
                    { "warehouse",BsonDocumentExpression.ArrayElemAt($"tmp_{MongoCollections.Warehouses}", 0) },
                    { "bin", BsonDocumentExpression.ArrayElemAt($"tmp_{MongoCollections.Bins}", 0) },
              })
              .As<StockDetails>();

            return await pipeline.FirstOrDefaultAsync(ct);
        }

    }
}
