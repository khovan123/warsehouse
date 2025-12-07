using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class Category
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = default!;
        [BsonElement("name")]
        public string Name { get; set; } = default!;
        [BsonElement("code")]
        public string Code { get; set; } = default!;
        [BsonElement("description")]
        public string? Description { get; set; } = default!;
        [BsonElement("group")]
        public string? Group { get; set; } = default!;
        [BsonElement("active")]
        public bool IsActive { get; set; } = default!;
    }
}
