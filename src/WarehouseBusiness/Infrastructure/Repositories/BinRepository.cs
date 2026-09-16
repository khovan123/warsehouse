using Domain.Entities;
using Domain.Entities.Weak;
using Domain.Repositories;
using Infrastructure.Constants;
using Infrastructure.DB;
using Infrastructure.Helpers;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Infrastructure.Repositories
{
    public class BinRepository : IBinRepository
    {
        private readonly IMongoCollection<Bin> _bin;

        public BinRepository(MongoDbContext context)
        {
            _bin = context.Bins;
        }

        public async Task<List<BinDetails>> GetAllAsync(CancellationToken ct)
        {
            var pipeline = new MongoAggregationPipeline<Bin>(_bin)
                .Match(b => true)
                .LookupAndUnwind(MongoCollections.Warehouses, "warehouseId", $"tmp_{MongoCollections.Warehouses}")
                .SetFields(new BsonDocument
                {
                    { "warehouseName", $"$tmp_{MongoCollections.Warehouses}.name" }
                })
                .Unset(new BsonArray { $"tmp_{MongoCollections.Warehouses}" })
                .As<BinDetails>();
            return await pipeline.ToListAsync(ct);
        }

        public async Task<BinDetails> GetByIdAsync(string id, CancellationToken ct)
        {
            var pipeline = new MongoAggregationPipeline<Bin>(_bin)
               .Match(b => string.Equals(b.Id, id, StringComparison.OrdinalIgnoreCase))
               .LookupAndUnwind(MongoCollections.Warehouses, "warehouseId", $"tmp_{MongoCollections.Warehouses}")
               .Unset(new BsonArray { $"tmp_{MongoCollections.Warehouses}" })
               .As<BinDetails>();
            return await pipeline.FirstOrDefaultAsync(ct);
        }
    }
}
