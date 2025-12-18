using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
  [BsonIgnoreExtraElements]
  public class Reservation
  {
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; } = default!;
    [BsonElement("reservationNo")]
    public string ReservationNo { get; set; } = default!;
    [BsonElement("productId")]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string RroductId { get; set; } = default!;
    [BsonElement("warehouseId")]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string WarehouseId { get; set; } = default!;
    [BsonElement("reservedQty")]
    public int ReservedQty { get; set; } = default!;
    [BsonElement("uom")]
    public string Uom { get; set; } = default!;
    [BsonElement("promisedDate")]
    public DateOnly PromisedDate { get; set; } = default!;
    [BsonElement("status")]
    public string Status { get; set; } = default!;
    [BsonElement("orderRef")]
    public string OrderRef { get; set; } = default!;
  }
}