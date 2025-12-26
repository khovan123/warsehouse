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

    public async Task<List<MovementReport>> GetAllWithDetails(CancellationToken ct)
    {
      static BsonDocument BuildProjection()
      {
        var inventoryType = BsonDocumentExpression.GetField(
          "type",
          BsonDocumentExpression.ArrayElemAt(
            MongoAggregationPipeline<Movement>.TmpCollectionName(MongoCollections.Inventory)
          )
        );

        return new BsonDocument
        {
          { "docNo", 1 },
          { "movementDate", 1 },
          { "productEntity", BsonDocumentExpression.ArrayElemAt(
            MongoAggregationPipeline<Movement>.TmpCollectionName(MongoCollections.Products))
          },
          { "fromWarehouse", BsonDocumentExpression.GetField(
            "code",
            BsonDocumentExpression.ArrayElemAt(
              MongoAggregationPipeline<Movement>.TmpCollectionName(MongoCollections.Warehouses))
          ) },
          { "toWarehouse", BsonDocumentExpression.GetField(
            "code",
            BsonDocumentExpression.ArrayElemAt("tmp_toWarehouses")
          ) },
          { "binEntity", BsonDocumentExpression.ArrayElemAt(
            MongoAggregationPipeline<Movement>.TmpCollectionName(MongoCollections.Bins))
          },
          { "type", inventoryType },
          { "qty", BsonDocumentExpression.Conditional(
            BsonDocumentExpression.In(inventoryType, new BsonArray { "Movement", "Shipment" }),
            BsonDocumentExpression.Multiply("$lines.qty", -1),
            "$lines.qty"
          ) }
        };
      }

      var pipeline = new MongoAggregationPipeline<Movement>(_movement)
        .UnwindField("lines", preserveNullAndEmptyArrays: false)
        .Lookup(MongoCollections.Products, "lines.product")
        .Lookup(MongoCollections.Warehouses, "fromWarehouse")
        .Lookup(MongoCollections.Warehouses, "toWarehouse", asAlias: "tmp_toWarehouses")
        .Lookup(MongoCollections.Bins, "lines.fromBin")
        .Lookup(MongoCollections.Inventory, "lines.product", "productId")
        .Project(BuildProjection())
        .As<MovementReport>();

      return await pipeline.ToListAsync(ct);
    }

    public async Task<List<MovementSummary>> GetSummary(SummaryPeriod period, CancellationToken ct)
    {
      static BsonDocument BuildCalculatedQtyProjection()
      {
        var inventoryType = BsonDocumentExpression.GetField(
          "type",
          BsonDocumentExpression.ArrayElemAt(
            MongoAggregationPipeline<Movement>.TmpCollectionName(MongoCollections.Inventory)
          )
        );

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

      BsonDocument BuildGroupSpec()
      {
        return new BsonDocument
        {
          { "_id", BsonDocumentExpression.BuildGroupIdByDateField(period, "movementDate") },
          { "negativeQty", BsonDocumentExpression.Sum(
            BsonDocumentExpression.Conditional(
              BsonDocumentExpression.Lt("$calculatedQty", 0),
              "$calculatedQty",
              0
            )
          ) },
          { "positiveQty", BsonDocumentExpression.Sum(
            BsonDocumentExpression.Conditional(
              BsonDocumentExpression.Gt("$calculatedQty", 0),
              "$calculatedQty",
              0
            )
          ) },
          { "totalQty", BsonDocumentExpression.Sum("$calculatedQty") }
        };
      }

      (DateTime startUtc, DateTime endUtc) = BsonDocumentExpression.RangeUtc(period);
      var startDate = DateOnly.FromDateTime(startUtc);
      var endDate = DateOnly.FromDateTime(endUtc);

      var result = new MongoAggregationPipeline<Movement>(_movement)
        .Match(m => m.MovementDate >= startDate && m.MovementDate < endDate)
        .UnwindField("lines", preserveNullAndEmptyArrays: false)
        .Lookup(MongoCollections.Inventory, "lines.product", "productId")
        .Project(BuildCalculatedQtyProjection())
        .Group(BuildGroupSpec())
        .Sort(new BsonDocument("_id", 1))
        .Project(new BsonDocument
        {
          { "_id", 0 },
          { "period", "$_id" },
          { "negativeQty", 1 },
          { "positiveQty", 1 },
          { "totalQty", 1 }
        })
        .As<MovementSummary>();

      return await result.ToListAsync(ct);
    }
  }
}