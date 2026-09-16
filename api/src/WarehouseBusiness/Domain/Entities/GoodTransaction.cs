using Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
  [BsonIgnoreExtraElements]
  public class GoodTransaction
  {
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; } = default!;
    [BsonElement("docNo")]
    public string DocNo { get; set; } = default!;
    [BsonElement("warehouseId")]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string WarehouseId { get; set; } = default!;
    [BsonElement("postedAt")]
    public DateOnly PostedAt { get; set; } = default!;
    [BsonElement("description")]
    public string Description { get; set; } = default!;
    [BsonElement("status")]
    public FlowStatus Status { get; set; } = default!;
    [BsonElement("createdBy")]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string CreatedBy { get; set; } = default!;
    [BsonElement("lines")]
    public List<GoodTransactionLine> Lines { get; set; } = default!;
  }

  public class GoodTransactionLine
  {
    [BsonElement("productId")]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string ProductId { get; set; } = default!;
    [BsonElement("expectedQty")]
    public int ExpectedQty { get; set; } = default!;
    [BsonElement("countedQty")]
    public int CountedQty { get; set; } = default!;
    [BsonElement("difference")]
    public int Difference { get; set; } = default!;
    [BsonElement("binId")]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string BinId { get; set; } = default!;
    [BsonElement("uom")]
    public string Uom { get; set; } = default!;
  }
}