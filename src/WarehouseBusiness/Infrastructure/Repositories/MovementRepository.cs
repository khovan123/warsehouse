using Domain.Entities;
using Domain.Repositories;
using Infrastructure.DB;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
  public class MovementRepository : IMovementRepository
  {
    private readonly IMongoCollection<Movement> _movement;

    public MovementRepository(MongoDbContext context)
    {
      _movement = context.Movements;
    }

    public async Task<List<Movement>> GetAll(CancellationToken ct)
    {
      var filter = Builders<Movement>.Filter.Empty;
      return await _movement.Find(filter).ToListAsync(ct);
    }

    public async Task<Movement> GetById(string id, CancellationToken ct)
    {
      var f = Builders<Movement>.Filter;
      var filter = f.And(
        f.Eq(m => m.Id, id)
      );
      return await _movement.Find(filter).FirstOrDefaultAsync(ct);
    }
  }
}