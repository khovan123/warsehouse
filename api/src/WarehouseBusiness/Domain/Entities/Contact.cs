using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class Contact
    {
        [BsonElement("person")]
        public string? Person { get; set; } = default!;
        [BsonElement("phone")]
        public string? Phone { get; set; } = default!;
        [BsonElement("email")]
        public string? Email { get; set; } = default!;
    }
}
