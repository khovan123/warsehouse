using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities.Weak
{
    [BsonIgnoreExtraElements]
    public class BinDetails: Bin
    {
        [BsonElement("warehouseName")]
        public string WarehouseName { get; set; } = default!;
    }
}
