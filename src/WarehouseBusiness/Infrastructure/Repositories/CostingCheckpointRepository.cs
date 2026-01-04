using MongoDB.Driver;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.DB;

namespace Infrastructure.Repositories
{
  public class CostingCheckpointRepository : ICostingCheckpointRepository
  {
    private readonly IMongoCollection<CostingCheckpoint> _costingCheckpoint;
    public CostingCheckpointRepository(MongoDbContext ctx)
    {
      _costingCheckpoint = ctx.CostingCheckpoints;
    }

    public async Task<CostingCheckpoint> GetOrCreateAsync(CancellationToken ct)
    {
      var filter = Builders<CostingCheckpoint>.Filter.Eq(x => x.Id, "global");
      var cp = await _costingCheckpoint.Find(filter).FirstOrDefaultAsync(ct);
      if (cp != null) return cp;

      cp = new CostingCheckpoint
      {
        Id = "global",
        LastProcessedSeq = 0,
        UpdatedAt = DateTime.UtcNow,
        Version = 1
      };

      try
      {
        await _costingCheckpoint.InsertOneAsync(cp, cancellationToken: ct);
      }
      catch (MongoWriteException ex) when (ex.WriteError?.Category == ServerErrorCategory.DuplicateKey)
      {
        cp = await _costingCheckpoint.Find(filter).FirstAsync(ct);
      }

      return cp;
    }

    public async Task UpdateOptimisticAsync(long newLastProcessedSeq, long expectedVersion, CancellationToken ct)
    {
      var filter = Builders<CostingCheckpoint>.Filter.Where(x => x.Id == "global" && x.Version == expectedVersion);
      var update = Builders<CostingCheckpoint>.Update
        .Set(x => x.LastProcessedSeq, newLastProcessedSeq)
        .Set(x => x.UpdatedAt, DateTime.UtcNow)
        .Inc(x => x.Version, 1);

      var res = await _costingCheckpoint.UpdateOneAsync(filter, update, cancellationToken: ct);
      if (res.ModifiedCount == 0)
        throw new InvalidOperationException("Optimistic concurrency conflict on CostingCheckpoint.");
    }
  }
}
