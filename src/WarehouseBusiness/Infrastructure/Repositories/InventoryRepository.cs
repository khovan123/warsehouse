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
    public class InventoryRepository : IInventoryRepository
    {
        private readonly IMongoCollection<Inventory> _inventory;

        public InventoryRepository(MongoDbContext context)
        {
            _inventory = context.Inventories;
        }

        public async Task<InventoryDetails> GetByIdAsync(string id, CancellationToken ct)
        {
            var pipeline = new MongoAggregationPipeline<Inventory>(_inventory)
                .Match(i => string.Equals(i.Id, id, StringComparison.OrdinalIgnoreCase))
                .LookupAndUnwind(MongoCollections.Warehouses, "warehouseId", $"tmp_{MongoCollections.Warehouses}")
                .LookupAndUnwind(MongoCollections.Bins, "binId", $"tmp_{MongoCollections.Bins}")
                .LookupAndUnwind(MongoCollections.Products, "productId", $"tmp_{MongoCollections.Products}")
                .LookupAndUnwind(MongoCollections.BusinessPartners, "bpartnerId", $"tmp_{MongoCollections.BusinessPartners}")
                .SetFields(
                        new BsonDocument{
                        { "WarehouseName", $"$tmp_{MongoCollections.Warehouses}.name" },
                        { "BinName", $"$tmp_{MongoCollections.Bins}.code" },
                        { "ProductName", $"$tmp_{MongoCollections.Products}.description" },
                        { "BusinessPartnerName", $"$tmp_{MongoCollections.BusinessPartners}.name" }
                    })
                .Project(
                    new BsonDocument{
                        {$"tmp_{MongoCollections.Warehouses}", 0},
                        {$"tmp_{MongoCollections.Bins}", 0},
                        {$"tmp_{MongoCollections.Products}", 0},
                        {$"tmp_{MongoCollections.BusinessPartners}", 0}
                    })
                .As<InventoryDetails>();

            return await pipeline.FirstOrDefaultAsync(ct);
        }

        public async Task<List<InventoryDetails>> GetAllAsync(CancellationToken ct)
        {
            var pipeline = new MongoAggregationPipeline<Inventory>(_inventory)
                .Match(i => true)
                .LookupAndUnwind(MongoCollections.Warehouses, "warehouseId", $"tmp_{MongoCollections.Warehouses}")
                .LookupAndUnwind(MongoCollections.Bins, "binId", $"tmp_{MongoCollections.Bins}")
                .LookupAndUnwind(MongoCollections.Products, "productId", $"tmp_{MongoCollections.Products}")
                .LookupAndUnwind(MongoCollections.BusinessPartners, "bpartnerId", $"tmp_{MongoCollections.BusinessPartners}")
                .SetFields(
                    new BsonDocument{
                        { "WarehouseName", $"$tmp_{MongoCollections.Warehouses}.name" },
                        { "BinName", $"$tmp_{MongoCollections.Bins}.code" },
                        { "ProductName", $"$tmp_{MongoCollections.Products}.description" },
                        { "BusinessPartnerName", $"$tmp_{MongoCollections.BusinessPartners}.name" }
                    })
                .Project(
                    new BsonDocument{
                        {$"tmp_{MongoCollections.Warehouses}", 0},
                        {$"tmp_{MongoCollections.Bins}", 0},
                        {$"tmp_{MongoCollections.Products}", 0},
                        {$"tmp_{MongoCollections.BusinessPartners}", 0}
                    })
                .As<InventoryDetails>();

            return await pipeline.ToListAsync(ct);
        }
    }
}
