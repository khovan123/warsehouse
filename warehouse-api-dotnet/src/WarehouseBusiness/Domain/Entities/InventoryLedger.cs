using Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
  [BsonIgnoreExtraElements]
  public class InventoryLedger
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = default!;

    [BsonElement("seq")]
    public long Seq { get; set; }

    [BsonElement("movementDate")]
    public DateTime MovementDate { get; set; } // UTC

    [BsonElement("postedAt")]
    public DateTime PostedAt { get; set; } // UTC

    [BsonElement("sourceType")]
    public SourceTypeEnum SourceType { get; set; } = default!; // INVENTORY | MOVEMENT | GOOD_TX

    [BsonElement("sourceId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string SourceId { get; set; } = default!;

    [BsonElement("docNo")]
    public string DocNo { get; set; } = default!;

    [BsonElement("lineNo")]
    public int LineNo { get; set; }

    [BsonElement("productId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ProductId { get; set; } = default!;

    [BsonElement("warehouseId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string WarehouseId { get; set; } = default!;

    [BsonElement("binId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string BinId { get; set; } = default!;

    [BsonElement("uom")]
    public string Uom { get; set; } = default!;

    [BsonElement("qty")]
    public int Qty { get; set; } // signed: +in / -out

    [BsonElement("unitCostHint")]
    public decimal? UnitCostHint { get; set; } // only for inbound usually

    [BsonElement("inventoryType")]
    public string? InventoryType { get; set; } // if SourceType=INVENTORY

    [BsonElement("reason")]
    public string? Reason { get; set; }

    [BsonElement("meta")]
    public BsonDocument? Meta { get; set; }
  }
}
