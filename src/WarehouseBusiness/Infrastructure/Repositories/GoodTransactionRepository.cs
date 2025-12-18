using Domain.Entities;
using Domain.Repositories;
using Infrastructure.DB;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
  public class GoodTransactionRepository : IGoodTransactionRepository
  {
    private readonly IMongoCollection<GoodTransaction> _goodTransaction;

    public GoodTransactionRepository(MongoDbContext context)
    {
      _goodTransaction = context.GoodTransactions;
    }

    public async Task<List<GoodTransaction>> GetAll(CancellationToken ct)
    {
      var filter = Builders<GoodTransaction>.Filter.Empty;
      return await _goodTransaction.Find(filter).ToListAsync(ct);
    }

    public async Task<GoodTransaction> GetById(string id, CancellationToken ct)
    {
      var f = Builders<GoodTransaction>.Filter;
      var filter = f.And(
        f.Eq(m => m.Id, id)
      );
      return await _goodTransaction.Find(filter).FirstOrDefaultAsync(ct);
    }
  }
}