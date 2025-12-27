using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Domain.Entities.Weak.Movement
{
  [BsonIgnoreExtraElements]
  public class MovementSummary
  {
    [BsonElement("period")]
    [JsonPropertyName("period")]
    public string Period { get; set; } = default!;

    [BsonElement("outbound")]
    [JsonPropertyName("outbound")]
    public int Outbound { get; set; } = default!;

    [BsonElement("inbound")]
    [JsonPropertyName("inbound")]
    public int Inbound { get; set; } = default!;

    [BsonElement("total")]
    [JsonPropertyName("total")]
    public int Total { get; set; } = default!;
  }
}
