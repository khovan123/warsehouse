using Domain.Entities;
using Domain.Repositories;
using Infrastructure.DB;
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

        public async Task<List<Stock>> GetAll(CancellationToken ct)
        {
            var filter = Builders<Stock>.Filter.Empty;
            return await _stocks.Find(filter).ToListAsync(ct);
        }

        public async Task<Stock?> GetById(string id, CancellationToken ct)
        {
            var filter = Builders<Stock>.Filter.Eq(s => s.Id, id);
            return await _stocks.Find(filter).FirstOrDefaultAsync(ct);
        }
    }
}
