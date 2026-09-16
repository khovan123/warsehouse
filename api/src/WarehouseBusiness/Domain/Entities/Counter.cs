using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
  [BsonIgnoreExtraElements]
  public class Counter
  {
    [BsonId]
    public string Id { get; set; } = default!; // e.g. "inventory_ledger_seq"

    [BsonElement("value")]
    public long Value { get; set; }
  }
}
