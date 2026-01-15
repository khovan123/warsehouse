using Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
  [BsonIgnoreExtraElements]
  public class OutboxEvent
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = default!;

    [BsonElement("kind")]
    public string Kind { get; set; } = "COSTING";

    [BsonElement("sourceType")]
    public string SourceType { get; set; } = default!;

    [BsonElement("sourceId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string SourceId { get; set; } = default!;

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("status")]
    public OutboxStatus Status { get; set; } = OutboxStatus.NEW; // NEW/PROCESSING/DONE/FAILED

    [BsonElement("retryCount")]
    public int RetryCount { get; set; }

    [BsonElement("lastError")]
    public string? LastError { get; set; }
  }
}
