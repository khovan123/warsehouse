using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
  [BsonIgnoreExtraElements]
  public class Movement
  {
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; } = default!;
    [BsonElement("docNo")]
    public string DocNo { get; set; } = default!;
    [BsonElement("movementDate")]
    public DateOnly MovementDate { get; set; } = default!;
    [BsonElement("fromWarehouse")]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string FromWarehouse { get; set; } = default!;
    [BsonElement("toWarehouse")]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string ToWarehouse { get; set; } = default!;
    [BsonElement("status")]
    public string Status { get; set; } = default!;
    [BsonElement("reason")]
    public string Reason { get; set; } = default!;
    [BsonElement("createdBy")]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string CreatedBy { get; set; } = default!;
    [BsonElement("lines")]
    public List<MovementLine> Lines { get; set; } = default!;
  }
  public class MovementLine
  {
    [BsonElement("product")]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string ProductId { get; set; } = default!;
    [BsonElement("qty")]
    public int Qty { get; set; } = default!;
    [BsonElement("uom")]
    public string Uom { get; set; } = default!;
    [BsonElement("fromBin")]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string FromBin { get; set; } = default!;
    [BsonElement("toBin")]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string ToBin { get; set; } = default!;
  }
}