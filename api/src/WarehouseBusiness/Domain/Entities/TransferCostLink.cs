using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
  [BsonIgnoreExtraElements]
  public class TransferCostLink
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = default!;

    [BsonElement("docNo")]
    public string DocNo { get; set; } = default!;

    [BsonElement("lineNo")]
    public int LineNo { get; set; }

    [BsonElement("productId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ProductId { get; set; } = default!;

    [BsonElement("unitCost")]
    public decimal UnitCost { get; set; }

    [BsonElement("outLedgerId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string OutLedgerId { get; set; } = default!;
  }
}
