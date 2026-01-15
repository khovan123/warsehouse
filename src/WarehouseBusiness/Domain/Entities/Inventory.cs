using Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
        [BsonIgnoreExtraElements]
        public class Inventory
        {
                [BsonId]
                [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
                public string Id { get; set; } = default!;
                [BsonElement("document")]
                public string Document { get; set; } = default!;
                [BsonElement("line")]
                public int Line { get; set; } = default!;
                [BsonElement("type")]
                public InventoryType Type { get; set; } = default!;
                [BsonElement("postedAt")]
                public DateOnly PostedAt { get; set; } = default!;
                [BsonElement("productId")]
                [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
                public string ProductId { get; set; } = default!;
                [BsonElement("warehouseId")]
                [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
                public string WarehouseId { get; set; } = default!;
                [BsonElement("binId")]
                [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
                public string BinId { get; set; } = default!;
                [BsonElement("qty")]
                public int Qty { get; set; } = default!;
                [BsonElement("uom")]
                public string Uom { get; set; } = default!;
                [BsonElement("unitCost")]
                public decimal UnitCost { get; set; } = default!;
                [BsonElement("bpartnerId")]
                [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
                public string BpartnerId { get; set; } = default!;

        }
}
