using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Domain.Entities.Weak
{
    [BsonIgnoreExtraElements]
    public class StockDetails : Stock
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

        [BsonIgnore]
        [JsonIgnore]
        public List<Product>? Products { get; set; }

        [BsonIgnore]
        [JsonIgnore]
        public List<Warehouse>? Warehouses { get; set; }

        [BsonIgnore]
        [JsonIgnore]
        public List<Bin>? Bins { get; set; }

        [BsonIgnore]
        [JsonIgnore]
        public List<Category>? Categories { get; set; }

        [BsonIgnore]
        [JsonPropertyName("product")]
        public string Product => ProductEntity != null ? $"{ProductEntity.Sku} {ProductEntity.Label}" : "Unknown Product";

        [BsonIgnore]
        [JsonPropertyName("warehouse")]
        public string Warehouse => WarehouseEntity?.Name ?? "Unknown Warehouse";

        [BsonIgnore]
        [JsonPropertyName("bin")]
        public string Bin => BinEntity?.Code ?? "Unknown Bin";
    }
}
