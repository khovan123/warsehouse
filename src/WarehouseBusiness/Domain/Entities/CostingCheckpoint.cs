using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
  [BsonIgnoreExtraElements]
  public class CostingCheckpoint
  {
    [BsonId]
    public string Id { get; set; } = "global"; // single row

    [BsonElement("lastProcessedSeq")]
    public long LastProcessedSeq { get; set; }

    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; }

    [BsonElement("version")]
    public long Version { get; set; }
  }
}
