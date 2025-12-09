using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Domain.Entities.Weak
{
    public class StockReport : Stock
    {
        [BsonElement("product")]
        [JsonIgnore]
        public Product? ProductEntity { get; set; }
        
        [BsonElement("warehouse")]
        [JsonIgnore]
        public Warehouse? WarehouseEntity { get; set; }
        
        [BsonElement("bin")]
        [JsonIgnore]
        public Bin? BinEntity { get; set; }
        
        [BsonElement("category")]
        [JsonIgnore]
        public Category? CategoryEntity { get; set; }
        
        [BsonIgnore]
        [JsonPropertyName("product")]
        public string Product => ProductEntity != null ? $"{ProductEntity.Sku} {ProductEntity.Label}" : "Unknown Product";
        
        [BsonIgnore]
        [JsonPropertyName("warehouse")]
        public string Warehouse => WarehouseEntity?.Name ?? "Unknown Warehouse";
        
        [BsonIgnore]
        [JsonPropertyName("bin")]
        public string Bin => BinEntity?.Code ?? "Unknown Bin";
        
        [BsonIgnore]
        [JsonPropertyName("category")]
        public string Category => CategoryEntity?.Name ?? "Unknown Category";
    }
}
