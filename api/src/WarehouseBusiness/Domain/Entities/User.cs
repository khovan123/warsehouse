using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class User
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = default!;
        [BsonElement("username")]
        public string Username { get; set; } = default!;
        [BsonElement("password")]
        public string? Password { get; set; } = default!;
        [BsonElement("passwordConfirm")]
        public string? PasswordConfirm { get; set; } = default!;
        [BsonElement("email")]
        public string Email { get; set; } = default!;
        [BsonElement("active")]
        public bool? IsActive { get; set; } = default!;
        [BsonElement("token")]
        public string? Token { get; set; } = default!;
        [BsonElement("signUpAt")]
        public DateTime? SignUpAt { get; set; } = default!;
    }
}
