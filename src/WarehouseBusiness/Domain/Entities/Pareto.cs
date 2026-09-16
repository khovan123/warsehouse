using Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class Pareto
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = default!;

        [BsonElement("productId")]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string ProductId { get; set; } = default!;

        [BsonElement("categoryId")]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string CategoryId { get; set; } = default!;

        [BsonElement("annualConsumption")]
        public double AnnualConsumption { get; set; }

        [BsonElement("value")]
        public double Value { get; set; }

        [BsonElement("tag")]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public ParetoTag? Tag { get; set; }
    }
}
