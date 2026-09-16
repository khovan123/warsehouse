using Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
  [BsonIgnoreExtraElements]
  public class ValuationEntry
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = default!;

    [BsonElement("ledgerId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string LedgerId { get; set; } = default!;

    [BsonElement("seq")]
    public long Seq { get; set; }

    [BsonElement("productId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ProductId { get; set; } = default!;

    [BsonElement("warehouseId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string WarehouseId { get; set; } = default!;

    [BsonElement("binId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string BinId { get; set; } = default!;

    [BsonElement("qty")]
    public int Qty { get; set; } // signed

    [BsonElement("unitCost")]
    public decimal UnitCost { get; set; }

    [BsonElement("amount")]
    public decimal Amount { get; set; } // signed qty*unitCost

    [BsonElement("method")]
    public string Method { get; set; } = "AVG";

    [BsonElement("postingDate")]
    public DateTime PostingDate { get; set; }

    [BsonElement("sourceType")]
    public SourceTypeEnum SourceType { get; set; } = default!;

    [BsonElement("docNo")]
    public string DocNo { get; set; } = default!;

    [BsonElement("lineNo")]
    public int LineNo { get; set; }

    [BsonElement("status")]
    public string Status { get; set; } = "VALUED";
  }
}
