using MongoDB.Driver;
using Domain.Entities;
using Infrastructure.DB;
using Domain.Enums;
using Domain.Repositories;

namespace Infrastructure.Repositories
{
  public class OutboxRepository : IOutboxRepository
  {
    private readonly IMongoCollection<OutboxEvent> _outbox;
    public OutboxRepository(MongoDbContext ctx)
    {
      _outbox = ctx.Outbox;
    }

    public async Task EnqueueCostingAsync(string sourceType, string sourceId, CancellationToken ct)
    {
      var ev = new OutboxEvent
      {
        Kind = "COSTING",
        SourceType = sourceType,
        SourceId = sourceId,
        CreatedAt = DateTime.UtcNow,
        Status = OutboxStatus.NEW,
        RetryCount = 0
      };
      await _outbox.InsertOneAsync(ev, cancellationToken: ct);
    }
  }
}
