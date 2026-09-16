using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class Product
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = default!;
        [BsonElement("accountId")]
        public string AccountId { get; set; } = default!;
        [BsonElement("label")]
        public string Label { get; set; } = default!;
        [BsonElement("sku")]
        public string Sku { get; set; } = default!;
        [BsonElement("baseUom")]
        public string BaseUom { get; set; } = default!;
        [BsonElement("description")]
        public string Description { get; set; } = default!;
        [BsonElement("overBook")]
        public bool IsOverBook { get; set; } = default!;
        [BsonElement("availability")]
        public string Availability { get; set; } = default!;
        [BsonElement("categoryId")]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string CategoryId { get; set; } = default!;
        [BsonElement("embedding")]
        public float[] Embedding { get; set; } = default!;
    }
}
