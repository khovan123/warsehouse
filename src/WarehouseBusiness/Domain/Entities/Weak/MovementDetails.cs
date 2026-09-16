using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities.Weak
{
    [BsonIgnoreExtraElements]
    public class MovementDetails
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = default!;
        [BsonElement("docNo")]
        public string DocNo { get; set; } = default!;
        [BsonElement("movementDate")]
        public DateOnly MovementDate { get; set; } = default!;
        [BsonElement("fromWarehouse")]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string FromWarehouse { get; set; } = default!;
        [BsonElement("toWarehouse")]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string ToWarehouse { get; set; } = default!;
        [BsonElement("status")]
        public string Status { get; set; } = default!;
        [BsonElement("reason")]
        public string Reason { get; set; } = default!;
        [BsonElement("createdBy")]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string CreatedBy { get; set; } = default!;
        [BsonElement("fromWarehouseName")]
        public string FromWarehouseName { get; set; } = default!;
        [BsonElement("toWarehouseName")]
        public string ToWarehouseName { get; set; } = default!;
        [BsonElement("lines")]
        public List<MovementLineDetail> Lines { get; set; } = default!;
    }

    public class MovementLineDetail : MovementLine
    {
        [BsonElement("productName")]
        public string ProductName { get; set; } = default!;
        [BsonElement("fromBinName")]
        public string FromBinName { get; set; } = default!;
        [BsonElement("toBinName")]
        public string ToBinName { get; set; } = default!;
    }
}
