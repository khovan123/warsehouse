using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities.Weak
{
    public class ProductWithCategory : Product
    {
        [BsonElement("category")]
        public Category? Category { get; set; } = default!;
    }
}
