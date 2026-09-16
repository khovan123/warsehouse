using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using Domain.Enums;

namespace Domain.Entities.Weak
{
    [BsonIgnoreExtraElements]
    public class ParetoReport : Pareto
    {
        [BsonElement("product")]
        [JsonIgnore]
        public Product? ProductEntity { get; set; }
        
        [BsonElement("category")]
        [JsonIgnore]
        public Category? CategoryEntity { get; set; }
        
        [BsonIgnore]
        [JsonIgnore]
        public List<Product>? Products { get; set; }
        
        [BsonIgnore]
        [JsonIgnore]
        public List<Category>? Categories { get; set; }
        
        [BsonIgnore]
        [JsonPropertyName("product")]
        public string Product => ProductEntity != null ? $"{ProductEntity.Sku} {ProductEntity.Description}" : "Unknown Product";

        [BsonIgnore]
        [JsonPropertyName("category")]
        public string Category => CategoryEntity?.Name ?? "Unknown Category";

        [BsonIgnore]
        [JsonIgnore]
        public ParetoTag? TagEnum => base.Tag;

        [BsonIgnore]
        [JsonPropertyName("tag")]
        public new string? Tag => base.Tag?.ToString();
    }
}
