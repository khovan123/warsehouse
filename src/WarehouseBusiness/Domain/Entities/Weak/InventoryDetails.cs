using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities.Weak
{
  [BsonIgnoreExtraElements]
  public class InventoryDetails : Inventory
  {
    [BsonElement("WarehouseName")]
    public string WarehouseName { get; set; } = default!;
    [BsonElement("ProductName")]
    public string ProductName { get; set; } = default!;
    [BsonElement("BinName")]
    public string BinName { get; set; } = default!;
    [BsonElement("BusinessPartnerName")]
    public string BusinessPartnerName { get; set; } = default!;
  }
}