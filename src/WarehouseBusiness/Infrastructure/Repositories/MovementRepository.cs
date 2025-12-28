using Domain.Entities;
using Domain.Entities.Weak.Movement;
using Domain.Enums;
using Domain.Helpers;
using Domain.Repositories;
using Infrastructure.Constants;
using Infrastructure.DB;
using Infrastructure.Helpers;
using MongoDB.Bson;
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
      var filter = Builders<Movement>.Filter.Eq(m => m.Id, id);
      return await _movement.Find(filter).FirstOrDefaultAsync(ct);
    }

    public async Task<List<MovementReport>> GetAllWithDetails(InventoryType? type, SummaryPeriod period, CancellationToken ct)
    {
      static BsonDocument BuildProjection()
      {
        var inventoryType = BsonDocumentExpression.GetField("type", BsonDocumentExpression.ArrayElemAt($"tmp_{MongoCollections.Inventory}"));

        return new BsonDocument
        {
          { "docNo", 1 },
          { "movementDate", 1 },
          { "productEntity", BsonDocumentExpression.ArrayElemAt($"tmp_{MongoCollections.Products}") },
          { "fromWarehouse", BsonDocumentExpression.GetField("code",BsonDocumentExpression.ArrayElemAt("tmp_fromWarehouses")) },
          { "toWarehouse", BsonDocumentExpression.GetField("code",BsonDocumentExpression.ArrayElemAt("tmp_toWarehouses")) },
          { "binEntity", BsonDocumentExpression.ArrayElemAt($"tmp_{MongoCollections.Bins}")},
          { "type", inventoryType },
          { "qty", BsonDocumentExpression.Conditional(
            BsonDocumentExpression.In(inventoryType, new BsonArray { MongoFields.Movement, MongoFields.Shipment }),
            BsonDocumentExpression.Multiply("$lines.qty", -1),
            "$lines.qty"
          ) }
        };
      }

      BsonDocument? BuildMatchFilter(InventoryType? filterType)
      {
        return BsonDocumentExpression.MatchEnumInArrayField(
          filterType,
          $"tmp_{MongoCollections.Inventory}",
          "type"
        );
      }

      (DateTime startUtc, DateTime endUtc) = BsonDocumentExpression.RangeUtc(period);
      var startDate = DateOnly.FromDateTime(startUtc);
      var endDate = DateOnly.FromDateTime(endUtc);

      var pipeline = new MongoAggregationPipeline<Movement>(_movement)
        .Match(m => m.MovementDate >= startDate && m.MovementDate < endDate)
        .Unwind("lines", false)
        .Lookup(MongoCollections.Products, "lines.product", $"tmp_{MongoCollections.Products}")
        .Lookup(MongoCollections.Warehouses, "fromWarehouse", "tmp_fromWarehouses")
        .Lookup(MongoCollections.Warehouses, "toWarehouse", "tmp_toWarehouses")
        .Lookup(MongoCollections.Bins, "lines.fromBin", $"tmp_{MongoCollections.Bins}")
        .Lookup(MongoCollections.Inventory, "lines.product", $"tmp_{MongoCollections.Inventory}", "productId");

      var matchFilter = BuildMatchFilter(type);
      if (matchFilter != null)
      {
        pipeline = pipeline.Match(matchFilter);
      }

      var result = pipeline
        .Project(BuildProjection())
        .As<MovementReport>();

      return await result.ToListAsync(ct);
    }

    public async Task<List<MovementSummary>> GetSummary(SummaryPeriod period, CancellationToken ct)
    {
      BsonDocument CalculatedQtyProjection()
      {
        var inventoryType = BsonDocumentExpression.GetField("type", BsonDocumentExpression.ArrayElemAt($"tmp_{MongoCollections.Inventory}"));

        return new BsonDocument
        {
          { "movementDate", 1 },
          { "calculatedQty", BsonDocumentExpression.Conditional(
            BsonDocumentExpression.In(inventoryType, new BsonArray { MongoFields.Movement, MongoFields.Shipment }),
            BsonDocumentExpression.Multiply("$lines.qty", -1),
            "$lines.qty"
          ) }
        };
      }

      BsonDocument GroupSpecByMovementDate()
      {
        return new BsonDocument
        {
          { "_id", BsonDocumentExpression.BuildGroupIdByDateField(period, "movementDate") },
          { "outbound", BsonDocumentExpression.Sum(
            BsonDocumentExpression.Conditional(
              BsonDocumentExpression.Lt("$calculatedQty", 0),
              "$calculatedQty",
              0)
          ) },
          { "inbound", BsonDocumentExpression.Sum(
            BsonDocumentExpression.Conditional(
              BsonDocumentExpression.Gt("$calculatedQty", 0),
              "$calculatedQty",
              0)
          ) },
          { "total", BsonDocumentExpression.Sum("$calculatedQty") }
        };
      }

      (DateTime startUtc, DateTime endUtc) = BsonDocumentExpression.RangeUtc(period);

      var startDate = DateOnly.FromDateTime(startUtc);
      var endDate = DateOnly.FromDateTime(endUtc);

      var result = new MongoAggregationPipeline<Movement>(_movement)
        .Match(m => m.MovementDate >= startDate && m.MovementDate < endDate)
        .Unwind("$lines", false)
        .Lookup(MongoCollections.Inventory, "lines.product", $"tmp_{MongoCollections.Inventory}", "productId")
        .Project(CalculatedQtyProjection())
        .Group(GroupSpecByMovementDate())
        .Sort(new BsonDocument("_id", 1))
        .Project(new BsonDocument
        {
          { "_id", 0 },
          { "period", "$_id" },
          { "outbound", 1 },
          { "inbound", 1 },
          { "total", 1 }
        })
        .As<MovementSummary>();

      return await result.ToListAsync(ct);
    }
  }
}