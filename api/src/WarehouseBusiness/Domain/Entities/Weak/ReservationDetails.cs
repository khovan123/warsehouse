using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities.Weak
{
  [BsonIgnoreExtraElements]
  public class ReservationDetails : Reservation
  {
    [BsonElement("productName")]
    public string ProductName { get; set; } = default!;
    [BsonElement("warehouseName")]
    public string WarehouseName { get; set; } = default!;
  }
}