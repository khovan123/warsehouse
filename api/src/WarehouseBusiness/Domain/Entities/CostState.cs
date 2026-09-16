using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
  [BsonIgnoreExtraElements]
  public class CostState
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = default!;

    [BsonElement("productId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ProductId { get; set; } = default!;

    [BsonElement("warehouseId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string WarehouseId { get; set; } = default!;

    [BsonElement("onHandQty")]
    public int OnHandQty { get; set; }

    [BsonElement("onHandValue")]
    public decimal OnHandValue { get; set; }

    [BsonElement("averageCost")]
    public decimal AverageCost { get; set; }

    [BsonElement("lastSeq")]
    public long LastSeq { get; set; }

    [BsonElement("version")]
    public long Version { get; set; }

    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; }
  }
}