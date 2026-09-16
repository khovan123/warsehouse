using Domain.Enums;

namespace Domain.Entities
{
  public class AccountingRule
  {
    public string Id { get; set; } = default!;
    public string SourceType { get; set; } = default!;   // INVENTORY / GOOD_TX / MOVEMENT
    public AccountingScenario Scenario { get; set; } = default!;     // RECEIPT/ISSUE/ADJ+/ADJ-/TRANSFER_OUT/TRANSFER_IN
    public string DebitAccount { get; set; } = default!;
    public string CreditAccount { get; set; } = default!;
    public bool UseInTransit { get; set; }
  }

}