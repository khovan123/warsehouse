using Domain.Entities;
using Domain.Repositories;
using Infrastructure.DB;
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
        public async Task<List<Inventory>> GetAll(CancellationToken ct)
        {
            var filter = Builders<Inventory>.Filter.Empty;
            return await _inventory.Find(filter).ToListAsync(ct);           
        }

        public async Task<Inventory> GetById(string id, CancellationToken ct)
        {
            var filter = Builders<Inventory>.Filter.Eq(i=>i.Id, id);
            return await _inventory.Find(filter).FirstOrDefaultAsync(ct);
        }
    }
}
