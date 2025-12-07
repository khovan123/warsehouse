using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities.Weak
{
    public class ProductWithCategory : Product
    {
        [BsonElement("category")]
        public Category? Category { get; set; } = default!;
        [BsonIgnore]
        public List<Category>? Categories { get; set; } = default!;
    }
}
