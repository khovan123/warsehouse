using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class Bin
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = default!;
        [BsonElement("warehouseId")]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string WarehouseId { get; set; } = default!;
        [BsonElement("code")]
        public string Code { get; set; } = default!;
        [BsonElement("description")]
        public string? Description { get; set; } = default!;
    }
}
