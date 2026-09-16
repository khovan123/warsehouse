using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities.Weak
{
  [BsonIgnoreExtraElements]
  public class GoodTransactionDetails
  {
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; } = default!;
    [BsonElement("docNo")]
    public string DocNo { get; set; } = default!;
    [BsonElement("warehouseId")]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string WarehouseId { get; set; } = default!;
    [BsonElement("warehouseName")]
    public string WarehouseName { get; set; } = default!;
    [BsonElement("countDate")]
    public DateOnly CountDate { get; set; } = default!;
    [BsonElement("description")]
    public string Description { get; set; } = default!;
    [BsonElement("status")]
    public string Status { get; set; } = default!;
    [BsonElement("createdBy")]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string CreatedBy { get; set; } = default!;
    [BsonElement("lines")]
    public List<GoodTransactionLineDetails> Lines { get; set; } = default!;
  }

  public class GoodTransactionLineDetails : GoodTransactionLine
  {
    [BsonElement("productName")]
    public string ProductName { get; set; } = default!;
    [BsonElement("binName")]
    public string BinName { get; set; } = default!;
  }
}