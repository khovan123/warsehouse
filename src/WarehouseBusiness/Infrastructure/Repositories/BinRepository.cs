using Domain.Entities;
using Domain.Repositories;
using Infrastructure.DB;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class BinRepository : IBinRepository
    {
        private readonly IMongoCollection<Bin> _bins;

        public BinRepository(MongoDbContext context)
        {
            _bins = context.Bins;
        }

        public async Task<List<Bin>> GetAll(CancellationToken ct)
        {
            var filter = Builders<Bin>.Filter.Empty;
            return await _bins.Find(filter).ToListAsync(ct);
        }

        public async Task<Bin> GetById(string id, CancellationToken ct)
        {
            var filter = Builders<Bin>.Filter.Eq(b => b.Id, id);
            return await _bins.Find(filter).FirstOrDefaultAsync(ct);
        }
    }
}
