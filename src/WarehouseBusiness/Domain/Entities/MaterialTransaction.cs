using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Domain.Entities.Weak.Movement
{
  [BsonIgnoreExtraElements]
  public class MaterialTransaction
  {
    [BsonElement("line")]
    public int Line { get; set; }

    [BsonElement("document")]
    [JsonPropertyName("document")]
    public string Document { get; set; } = default!;

    [BsonElement("movementDate")]
    [JsonPropertyName("movementDate")]
    public DateOnly MovementDate { get; set; }

    [BsonElement("type")]
    [JsonPropertyName("type")]
    public string Type { get; set; } = default!;

    [BsonElement("productEntity")]
    [JsonIgnore]
    public Product? ProductEntity { get; set; }

    [BsonIgnore]
    [JsonPropertyName("product")]
    public string Product => ProductEntity != null ? $"{ProductEntity.Sku} {ProductEntity.Description}" : "Unknown Product";
    
    [BsonElement("fromWarehouse")]
    [JsonPropertyName("fromWarehouse")]
    public string FromWarehouse { get; set; } = default!;

    [BsonElement("binEntity")]
    [JsonIgnore]
    public Bin? BinEntity { get; set; }
    
    [BsonIgnore]
    [JsonPropertyName("bin")]
    public string Bin => BinEntity?.Code ?? "Unknown Bin";

    [BsonElement("qty")]
    [JsonPropertyName("qty")]
    public int Qty { get; set; }

    [BsonElement("uom")]
    [JsonPropertyName("uom")]
    public string UOM { get; set; } = default!;

    [BsonElement("cost")]
    public double Cost { get; set; }

    [BsonElement("businessPartner")]
    [JsonPropertyName("businessPartner")]
    public string BusinessPartner { get; set; } = default!;
  }
}
