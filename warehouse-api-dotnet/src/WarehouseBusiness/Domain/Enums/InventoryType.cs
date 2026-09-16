using System.Text.Json.Serialization;

namespace Domain.Enums
{
  [JsonConverter(typeof(JsonStringEnumConverter))]
  public enum InventoryType
  {
    Receipt = 0,
    Shipment = 1,
    Movement = 2,
    Inventory = 3,
    Adjustment = 4
  }
}
