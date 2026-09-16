using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class BusinessPartner
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = default!;
        [BsonElement("name")]
        public string Name { get; set; } = default!;
        [BsonElement("type")]
        public string Type { get; set; } = default!;
        [BsonElement("organization")]
        public string Organization { get; set; } = default!;
        [BsonElement("active")]
        public bool IsActive { get; set; } = default!;
        [BsonElement("contact")]
        public Contact? Contact { get; set; } = default!;
        [BsonElement("address")]
        public Address? Address { get; set; } = default!;
    }
}
