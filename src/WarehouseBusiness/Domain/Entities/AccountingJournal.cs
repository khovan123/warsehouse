using Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
  [BsonIgnoreExtraElements]
  public class AccountingJournal
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = default!;

    [BsonElement("sourceValuationId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string SourceValuationId { get; set; } = default!;

    [BsonElement("docNo")]
    public string DocNo { get; set; } = default!;

    [BsonElement("postingDate")]
    public DateTime PostingDate { get; set; }

    [BsonElement("status")]
    public AccountingStatus Status { get; set; } = AccountingStatus.POSTED;

    [BsonElement("lines")]
    public List<AccountingLine> Lines { get; set; } = default!;
  }

  public class AccountingLine
  {
    [BsonElement("account")]
    public string Account { get; set; } = default!;

    [BsonElement("debit")]
    public decimal Debit { get; set; }

    [BsonElement("credit")]
    public decimal Credit { get; set; }

    [BsonElement("currency")]
    public string? Currency { get; set; }
  }
}
