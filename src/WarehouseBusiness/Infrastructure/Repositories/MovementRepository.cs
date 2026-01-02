using Domain.Entities;
using Domain.Entities.Weak;
using Domain.Entities.Weak.Movement;
using Domain.Enums;
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

    public async Task<List<MovementDetails>?> GetAllAsync(CancellationToken ct)
    {
      var pipeline = new MongoAggregationPipeline<Movement>(_movement)
        .Match(m => true)
        .Unwind("$lines", false)
        .LookupAndUnwind(MongoCollections.Warehouses, "fromWarehouse", $"tmp_from{MongoCollections.Warehouses}")
        .LookupAndUnwind(MongoCollections.Warehouses, "toWarehouse", $"tmp_to{MongoCollections.Warehouses}")
        .LookupAndUnwind(MongoCollections.Products, "lines.product", $"tmp_{MongoCollections.Products}")
        .LookupAndUnwind(MongoCollections.Bins, "lines.fromBin", $"tmp_from{MongoCollections.Bins}")
        .LookupAndUnwind(MongoCollections.Bins, "lines.toBin", $"tmp_to{MongoCollections.Bins}")
        .SetFields(new BsonDocument
        {
          {"lines.productName",$"$tmp_{MongoCollections.Products}.description"},
          {"fromWarehouseName",$"$tmp_from{MongoCollections.Warehouses}.name"},
          {"toWarehouseName",$"$tmp_to{MongoCollections.Warehouses}.name"},
          {"lines.fromBinName",$"$tmp_from{MongoCollections.Bins}.code"},
          {"lines.toBinName",$"$tmp_to{MongoCollections.Bins}.code"},
        })
        .Unset(new BsonArray {
          $"tmp_{MongoCollections.Products}",
          $"tmp_from{MongoCollections.Warehouses}",
          $"tmp_to{MongoCollections.Warehouses}",
          $"tmp_from{MongoCollections.Bins}",
          $"tmp_to{MongoCollections.Bins}"
        })
        .Group(new BsonDocument
        {
          { "_id", "$_id" },
          { "docNo", new BsonDocument("$first", "$docNo") },
          { "movementDate", new BsonDocument("$first", "$movementDate") },
          { "fromWarehouse", new BsonDocument("$first", "$fromWarehouse") },
          { "fromWarehouseName", new BsonDocument("$first", "$fromWarehouseName") },
          { "toWarehouse", new BsonDocument("$first", "$toWarehouse") },
          { "toWarehouseName", new BsonDocument("$first", "$toWarehouseName") },
          { "status", new BsonDocument("$first", "$status") },
          { "reason", new BsonDocument("$first", "$reason") },
          { "createdBy", new BsonDocument("$first", "$createdBy") },
          { "lines", new BsonDocument("$push", "$lines") }
        })
        .As<MovementDetails>();
      return await pipeline.ToListAsync(ct);
    }

    public async Task<MovementDetails?> GetByIdAsync(string id, CancellationToken ct)
    {
      var pipeline = new MongoAggregationPipeline<Movement>(_movement)
        .Match(m => string.Equals(m.Id, id, StringComparison.OrdinalIgnoreCase))
        .Unwind("$lines", false)
        .LookupAndUnwind(MongoCollections.Warehouses, "fromWarehouse", $"tmp_from{MongoCollections.Warehouses}")
        .LookupAndUnwind(MongoCollections.Warehouses, "toWarehouse", $"tmp_to{MongoCollections.Warehouses}")
        .LookupAndUnwind(MongoCollections.Products, "lines.product", $"tmp_{MongoCollections.Products}")
        .LookupAndUnwind(MongoCollections.Bins, "lines.fromBin", $"tmp_from{MongoCollections.Bins}")
        .LookupAndUnwind(MongoCollections.Bins, "lines.toBin", $"tmp_to{MongoCollections.Bins}")
        .SetFields(new BsonDocument
        {
          {"lines.productName",$"$tmp_{MongoCollections.Products}.description"},
          {"fromWarehouseName",$"$tmp_from{MongoCollections.Warehouses}.name"},
          {"toWarehouseName",$"$tmp_to{MongoCollections.Warehouses}.name"},
          {"lines.fromBinName",$"$tmp_from{MongoCollections.Bins}.code"},
          {"lines.toBinName",$"$tmp_to{MongoCollections.Bins}.code"},
        })
        .Unset(new BsonArray {
          $"tmp_{MongoCollections.Products}",
          $"tmp_from{MongoCollections.Warehouses}",
          $"tmp_to{MongoCollections.Warehouses}",
          $"tmp_from{MongoCollections.Bins}",
          $"tmp_to{MongoCollections.Bins}"
        })
        .Group(new BsonDocument
        {
          { "_id", "$_id" },
          { "docNo", new BsonDocument("$first", "$docNo") },
          { "movementDate", new BsonDocument("$first", "$movementDate") },
          { "fromWarehouse", new BsonDocument("$first", "$fromWarehouse") },
          { "fromWarehouseName", new BsonDocument("$first", "$fromWarehouseName") },
          { "toWarehouse", new BsonDocument("$first", "$toWarehouse") },
          { "toWarehouseName", new BsonDocument("$first", "$toWarehouseName") },
          { "status", new BsonDocument("$first", "$status") },
          { "reason", new BsonDocument("$first", "$reason") },
          { "createdBy", new BsonDocument("$first", "$createdBy") },
          { "lines", new BsonDocument("$push", "$lines") }
        })
        .As<MovementDetails>();
      return await pipeline.FirstOrDefaultAsync(ct);
    }

    public async Task<List<MovementReport>> GetAllWithDetailsAsync(InventoryType? type, SummaryPeriod period, CancellationToken ct)
    {
      var inventoryTypeDoc = BsonDocumentExpression.GetField("type", BsonDocumentExpression.ArrayElemAt($"tmp_{MongoCollections.Inventory}"));

      var matchTypeDoc = BsonDocumentExpression.MatchInArrayField(
          type,
          $"tmp_{MongoCollections.Inventory}",
          "type"
        );

      (DateTime startUtc, DateTime endUtc) = DateHelper.CreateDateRangeUtc(period: period);

      var startDate = DateOnly.FromDateTime(startUtc);
      var endDate = DateOnly.FromDateTime(endUtc);

      var pipeline = new MongoAggregationPipeline<Movement>(_movement)
        .Match(m => m.MovementDate >= startDate && m.MovementDate < endDate)
        .Unwind("lines", false)
        .Lookup(MongoCollections.Products, "lines.product", $"tmp_{MongoCollections.Products}")
        .Lookup(MongoCollections.Warehouses, "fromWarehouse", "tmp_fromWarehouses")
        .Lookup(MongoCollections.Warehouses, "toWarehouse", "tmp_toWarehouses")
        .Lookup(MongoCollections.Bins, "lines.fromBin", $"tmp_{MongoCollections.Bins}")
        .Lookup(MongoCollections.Inventory, "lines.product", $"tmp_{MongoCollections.Inventory}", "productId")
        .Optional(matchTypeDoc)
        .Project(new BsonDocument
        {
          { "docNo", 1 },
          { "movementDate", 1 },
          { "productEntity", BsonDocumentExpression.ArrayElemAt($"tmp_{MongoCollections.Products}") },
          { "fromWarehouse", BsonDocumentExpression.GetField("code",BsonDocumentExpression.ArrayElemAt("tmp_fromWarehouses")) },
          { "toWarehouse", BsonDocumentExpression.GetField("code",BsonDocumentExpression.ArrayElemAt("tmp_toWarehouses")) },
          { "binEntity", BsonDocumentExpression.ArrayElemAt($"tmp_{MongoCollections.Bins}")},
          { "type", inventoryTypeDoc },
          { "qty", BsonDocumentExpression.Conditional(
            BsonDocumentExpression.In(inventoryTypeDoc, new BsonArray { MongoFields.Movement, MongoFields.Shipment }),
            BsonDocumentExpression.Multiply("$lines.qty", -1),
            "$lines.qty"
          ) }
        })
        .As<MovementReport>();

      return await pipeline.ToListAsync(ct);
    }

    public async Task<List<MovementSummary>> GetSummaryAsync(SummaryPeriod period, CancellationToken ct)
    {
      var inventoryType = BsonDocumentExpression.GetField("type", BsonDocumentExpression.ArrayElemAt($"tmp_{MongoCollections.Inventory}"));

      var calculatedQtyDoc = new BsonDocument
        {
          { "movementDate", 1 },
          { "calculatedQty", BsonDocumentExpression.Conditional(
            BsonDocumentExpression.In(inventoryType, new BsonArray { MongoFields.Movement, MongoFields.Shipment }),
            BsonDocumentExpression.Multiply("$lines.qty", -1),
            "$lines.qty"
          ) }
        };

      (DateTime startUtc, DateTime endUtc) = DateHelper.CreateDateRangeUtc(period: period);

      var startDate = DateOnly.FromDateTime(startUtc);
      var endDate = DateOnly.FromDateTime(endUtc);

      var result = new MongoAggregationPipeline<Movement>(_movement)
        .Match(m => m.MovementDate >= startDate && m.MovementDate < endDate)
        .Unwind("$lines", false)
        .Lookup(MongoCollections.Inventory, "lines.product", $"tmp_{MongoCollections.Inventory}", "productId")
        .Project(calculatedQtyDoc)
        .Group(new BsonDocument
        {
          { "_id", BsonDocumentExpression.BuildGroupIdByDateField(period, "movementDate") },
          { "outbound", BsonDocumentExpression.Sum(
            BsonDocumentExpression.Conditional( BsonDocumentExpression.Lt("$calculatedQty", 0), "$calculatedQty", 0))
          },
          { "inbound", BsonDocumentExpression.Sum(
            BsonDocumentExpression.Conditional( BsonDocumentExpression.Gt("$calculatedQty", 0),"$calculatedQty", 0))
          },
          { "total", BsonDocumentExpression.Sum("$calculatedQty") }
        })
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

    public async Task<List<MaterialTransaction>> GetMaterialTransactionAsync(InventoryType? type, string? warehouseId, CancellationToken ct)
    {
      static BsonDocument BuildProjection()
      {
        return new BsonDocument
        {
          { "document", "$docNo" },
          { "movementDate", 1 },
          { "type", BsonDocumentExpression.GetField("type", BsonDocumentExpression.ArrayElemAt($"tmp_{MongoCollections.Inventory}")) },
          { "productEntity", BsonDocumentExpression.ArrayElemAt($"tmp_{MongoCollections.Products}") },
          { "fromWarehouse", BsonDocumentExpression.GetField("code", BsonDocumentExpression.ArrayElemAt("tmp_fromWarehouses")) },
          { "binEntity", BsonDocumentExpression.ArrayElemAt($"tmp_{MongoCollections.Bins}") },
          { "qty", "$lines.qty" },
          { "cost", BsonDocumentExpression.GetField("cost", BsonDocumentExpression.ArrayElemAt($"tmp_{MongoCollections.Inventory}")) },
          { "uom", BsonDocumentExpression.GetField("baseUom", BsonDocumentExpression.ArrayElemAt($"tmp_{MongoCollections.Products}")) },
          { "businessPartner", BsonDocumentExpression.GetField("name", BsonDocumentExpression.ArrayElemAt($"tmp_{MongoCollections.BusinessPartners}")) }
        };
      }

      BsonDocument? BuildTypeMatchFilter(InventoryType? filterType)
      {
        return BsonDocumentExpression.MatchInArrayField(
          filterType,
          $"tmp_{MongoCollections.Inventory}",
          "type"
        );
      }

      var pipeline = new MongoAggregationPipeline<Movement>(_movement);

      if (!string.IsNullOrEmpty(warehouseId))
      {
        pipeline = pipeline.Match(m => m.FromWarehouse == warehouseId);
      }
      else
      {
        pipeline = pipeline.Match(m => true);
      }

      pipeline = pipeline
        .Unwind("lines", "lineIndex", false)
        .Lookup(MongoCollections.Products, "lines.product", $"tmp_{MongoCollections.Products}")
        .Lookup(MongoCollections.Warehouses, "fromWarehouse", "tmp_fromWarehouses")
        .Lookup(MongoCollections.Bins, "lines.fromBin", $"tmp_{MongoCollections.Bins}")
        .Lookup(MongoCollections.Inventory, "lines.product", $"tmp_{MongoCollections.Inventory}", "productId");

      var inventoryTypeMatchFilter = BuildTypeMatchFilter(type);
      if (inventoryTypeMatchFilter != null)
      {
        pipeline = pipeline.Match(inventoryTypeMatchFilter);
      }

      var result = pipeline
        .LookupWithPipeline(
          MongoCollections.BusinessPartners,
          BsonDocumentExpression.Let(
            "bpId",
            BsonDocumentExpression.GetField(
              "bpartnerId",
              BsonDocumentExpression.ArrayElemAt($"tmp_{MongoCollections.Inventory}")
            )
          ),
          BsonDocumentExpression.LookupMatchById("bpId"),
          $"tmp_{MongoCollections.BusinessPartners}"
        )
        .Project(BuildProjection())
        .Sort(new BsonDocument("movementDate", 1))
        .SetWindowFields(
          new BsonDocument
          {
            { "sortBy", new BsonDocument("movementDate", 1) },
            { "output", new BsonDocument
              {
                { "line", new BsonDocument("$documentNumber", new BsonDocument()) }
              }
            }
          }
        )
        .As<MaterialTransaction>();

      return await result.ToListAsync(ct);
    }
  }
}