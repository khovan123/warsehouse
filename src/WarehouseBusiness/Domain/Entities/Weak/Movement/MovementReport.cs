using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Domain.Entities.Weak.Movement
{
  [BsonIgnoreExtraElements]
  public class MovementReport
  {
    [BsonElement("docNo")]
    [JsonPropertyName("docNo")]
    public string DocNo { get; set; } = default!;
    
    [BsonElement("movementDate")]
    [JsonPropertyName("movementDate")]
    public DateOnly MovementDate { get; set; }
    
    [BsonElement("productEntity")]
    [JsonIgnore]
    public Product? ProductEntity { get; set; }
    
    [BsonIgnore]
    [JsonPropertyName("product")]
    public string Product => ProductEntity != null ? $"{ProductEntity.Sku} {ProductEntity.Description}" : "Unknown Product";
    
    [BsonElement("fromWarehouse")]
    [JsonPropertyName("fromWarehouse")]
    public string FromWarehouse { get; set; } = default!;
    
    [BsonElement("toWarehouse")]
    [JsonPropertyName("toWarehouse")]
    public string ToWarehouse { get; set; } = default!;
    
    [BsonElement("binEntity")]
    [JsonIgnore]
    public Bin? BinEntity { get; set; }
    
    [BsonIgnore]
    [JsonPropertyName("bin")]
    public string Bin => BinEntity?.Code ?? "Unknown Bin";
    
    [BsonElement("type")]
    [JsonPropertyName("type")]
    public string Type { get; set; } = default!;
    
    [BsonElement("qty")]
    [JsonPropertyName("qty")]
    public int Qty { get; set; }
  }
}
