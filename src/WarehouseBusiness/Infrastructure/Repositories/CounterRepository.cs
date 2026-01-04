using Domain.Entities;
using Domain.Repositories;
using Infrastructure.DB;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
  public class CounterRepository : ICounterRepository
  {
    private readonly IMongoCollection<Counter> _counter;

    public CounterRepository(MongoDbContext context)
    {
      _counter = context.Counters;
    }

    public async Task<long> NextAsync(string counterId, CancellationToken ct)
    {
      var filter = Builders<Counter>.Filter.Eq(x => x.Id, counterId);
      var update = Builders<Counter>.Update.Inc(x => x.Value, 1);
      var options = new FindOneAndUpdateOptions<Counter>
      {
        IsUpsert = true,
        ReturnDocument = ReturnDocument.After
      };

      var doc = await _counter.FindOneAndUpdateAsync(filter, update, options, ct);
      return doc.Value;
    }
  }
}