using Domain.Entities;
using Domain.Entities.Weak;
using Domain.Repositories;
using Infrastructure.Constants;
using Infrastructure.DB;
using Infrastructure.Helpers;

// using Infrastructure.Helpers;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly IMongoCollection<Inventory> _inventory;

        public InventoryRepository(MongoDbContext context)
        {
            _inventory = context.Inventories;
        }
        // public async Task<List<Inventory>> GetAll(CancellationToken ct)
        // {
        //     var filter = Builders<Inventory>.Filter.Empty;
        //     return await _inventory.Find(filter).ToListAsync(ct);
        // }

        public async Task<InventoryDetails> GetById(string id, CancellationToken ct)
        {
            var pipeline = new MongoAggregationPipeline<Inventory>(_inventory)
                .Match(i => string.Equals(i.Id, id, StringComparison.OrdinalIgnoreCase))
                .LookupAndUnwind(MongoCollections.Warehouses, "warehouseId")
                .LookupAndUnwind(MongoCollections.Bins, "binId")
                .LookupAndUnwind(MongoCollections.Products, "productId")
                .LookupAndUnwind(MongoCollections.BusinessPartners, "bpartnerId")
                .SetFields(
                    new BsonDocument{
                        { "WarehouseName", MongoAggregationPipeline<Inventory>.RefField(MongoCollections.Warehouses,"name") },
                        { "BinName", MongoAggregationPipeline<Inventory>.RefField(MongoCollections.Bins,"code") },
                        { "ProductName", MongoAggregationPipeline<Inventory>.RefField(MongoCollections.Products,"description") },
                        { "BusinessPartnerName", MongoAggregationPipeline<Inventory>.RefField(MongoCollections.BusinessPartners,"name") }
                    })
                .Project(
                    new BsonDocument{
                        {MongoAggregationPipeline<Inventory>.TmpCollectionName(MongoCollections.Warehouses), 0},
                        {MongoAggregationPipeline<Inventory>.TmpCollectionName(MongoCollections.Bins), 0},
                        {MongoAggregationPipeline<Inventory>.TmpCollectionName(MongoCollections.Products), 0},
                        {MongoAggregationPipeline<Inventory>.TmpCollectionName(MongoCollections.BusinessPartners), 0}
                    })
                .As<InventoryDetails>();

            return await pipeline.FirstOrDefaultAsync(ct);
        }

        public async Task<List<InventoryDetails>> GetAll(CancellationToken ct)
        {
            var pipeline = new MongoAggregationPipeline<Inventory>(_inventory)
                .Match(i => true)
                .LookupAndUnwind(MongoCollections.Warehouses, "warehouseId")
                .LookupAndUnwind(MongoCollections.Bins, "binId")
                .LookupAndUnwind(MongoCollections.Products, "productId")
                .LookupAndUnwind(MongoCollections.BusinessPartners, "bpartnerId")
                .SetFields(
                    new BsonDocument{
                        { "WarehouseName", MongoAggregationPipeline<Inventory>.RefField(MongoCollections.Warehouses,"name") },
                        { "BinName", MongoAggregationPipeline<Inventory>.RefField(MongoCollections.Bins,"code") },
                        { "ProductName", MongoAggregationPipeline<Inventory>.RefField(MongoCollections.Products,"description") },
                        { "BusinessPartnerName", MongoAggregationPipeline<Inventory>.RefField(MongoCollections.BusinessPartners,"name") }
                    })
                .Project(
                    new BsonDocument{
                        {MongoAggregationPipeline<Inventory>.TmpCollectionName(MongoCollections.Warehouses), 0},
                        {MongoAggregationPipeline<Inventory>.TmpCollectionName(MongoCollections.Bins), 0},
                        {MongoAggregationPipeline<Inventory>.TmpCollectionName(MongoCollections.Products), 0},
                        {MongoAggregationPipeline<Inventory>.TmpCollectionName(MongoCollections.BusinessPartners), 0}
                    })
                .As<InventoryDetails>();

            return await pipeline.ToListAsync(ct);
        }
    }
}
