using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class RefreshToken
    {
        [BsonElement("userId")]
        public string UserId { get; set; } = default!;
        [BsonElement("tokenHash")]
        public string TokenHash { get; set; } = default!;
        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }
        [BsonElement("expiresAt")]
        public DateTime ExpiresAt { get; set; }
        [BsonElement("revokedAt")]
        public DateTime? RevokedAt { get; set; }
        [BsonElement("replacedByTokenHash")]
        public string? ReplacedByTokenHash { get; set; }
        [BsonElement("deviceId")]
        public string? DeviceId { get; set; }
    }
}
