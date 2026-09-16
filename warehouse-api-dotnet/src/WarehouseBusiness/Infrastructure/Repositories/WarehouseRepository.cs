using Domain.Entities;
using Domain.Repositories;
using Infrastructure.DB;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly IMongoCollection<Warehouse> _warehouses;

        public WarehouseRepository(MongoDbContext context)
        {
            _warehouses = context.Warehouses;
        }

        public async Task<List<Warehouse>?> GetAllAsync(CancellationToken ct)
        {
            var filter = Builders<Warehouse>.Filter.Eq(w => w.IsActive, true);
            return await _warehouses.Find(filter).ToListAsync(ct);
        }

        public async Task<Warehouse?> GetByIdAsync(string id, CancellationToken ct)
        {
            var exp = Builders<Warehouse>.Filter;
            var filter = exp.And(
                exp.Eq(w => w.Id, id),
                exp.Eq(w => w.IsActive, true)
                );
            return await _warehouses.Find(filter).FirstOrDefaultAsync(ct);
        }
    }
}
