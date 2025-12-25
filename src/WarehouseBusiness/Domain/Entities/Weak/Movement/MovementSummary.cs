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
    
    [BsonElement("negativeQty")]
    [JsonPropertyName("negativeQty")]
    public int NegativeQty { get; set; }
    
    [BsonElement("positiveQty")]
    [JsonPropertyName("positiveQty")]
    public int PositiveQty { get; set; }
    
    [BsonElement("totalQty")]
    [JsonPropertyName("totalQty")]
    public int TotalQty { get; set; }
  }
}
