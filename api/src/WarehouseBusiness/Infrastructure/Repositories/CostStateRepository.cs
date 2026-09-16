using MongoDB.Driver;
using Domain.Entities;
using Infrastructure.DB;

using Domain.Repositories;

namespace Infrastructure.Repositories
{
  public class CostStateRepository : ICostStateRepository
  {
    private readonly IMongoCollection<CostState> _costState;
    public CostStateRepository(MongoDbContext ctx)
    {
      _costState = ctx.CostStates;
    }

    public async Task<CostState> GetOrCreateAsync(string productId, string warehouseId, CancellationToken ct)
    {
      var filter = Builders<CostState>.Filter.Where(x => x.ProductId == productId && x.WarehouseId == warehouseId);
      var state = await _costState.Find(filter).FirstOrDefaultAsync(ct);
      if (state != null) return state;

      state = new CostState
      {
        ProductId = productId,
        WarehouseId = warehouseId,
        OnHandQty = 0,
        OnHandValue = 0,
        AverageCost = 0,
        LastSeq = 0,
        Version = 1,
        UpdatedAt = DateTime.UtcNow
      };

      try
      {
        await _costState.InsertOneAsync(state, cancellationToken: ct);
      }
      catch (MongoWriteException ex) when (ex.WriteError?.Category == ServerErrorCategory.DuplicateKey)
      {
        // another writer inserted
        state = await _costState.Find(filter).FirstAsync(ct);
      }
      return state;
    }

    public async Task UpdateOptimisticAsync(CostState state, long expectedVersion, CancellationToken ct)
    {
      var filter = Builders<CostState>.Filter.Where(x =>
        x.ProductId == state.ProductId && x.WarehouseId == state.WarehouseId && x.Version == expectedVersion);

      var update = Builders<CostState>.Update
        .Set(x => x.OnHandQty, state.OnHandQty)
        .Set(x => x.OnHandValue, state.OnHandValue)
        .Set(x => x.AverageCost, state.AverageCost)
        .Set(x => x.LastSeq, state.LastSeq)
        .Set(x => x.UpdatedAt, state.UpdatedAt)
        .Inc(x => x.Version, 1);

      var res = await _costState.UpdateOneAsync(filter, update, cancellationToken: ct);
      if (res.ModifiedCount == 0)
        throw new InvalidOperationException("Optimistic concurrency conflict on CostState.");
    }
  }
}
