using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class Stock
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = default!;
        
        [BsonElement("productId")]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string ProductId { get; set; } = default!;
        
        [BsonElement("warehouseId")]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string WarehouseId { get; set; } = default!;
        
        [BsonElement("binId")]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string BinId { get; set; } = default!;
        
        [BsonElement("categoryId")]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string CategoryId { get; set; } = default!;
        
        [BsonElement("onHand")]
        public int OnHand { get; set; }
        
        [BsonElement("reserved")]
        public int Reserved { get; set; }
        
        [BsonElement("available")]
        public int Available { get; set; }
        
        [BsonElement("averageCost")]
        public decimal AverageCost { get; set; }
        
        [BsonElement("inventoryValue")]
        public decimal InventoryValue { get; set; }
    }
}
