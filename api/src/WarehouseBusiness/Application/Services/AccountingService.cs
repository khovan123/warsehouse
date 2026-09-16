using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;

namespace Application.Services
{
  public class AccountingService : IAccountingService
  {
    private readonly IJournalRepository _journalRepo;
    public AccountingService(IJournalRepository journalRepo) => _journalRepo = journalRepo;

    // Stub: bạn sẽ mapping account theo rule sau. Ở đây demo:
    public async Task PostFromValuationAsync(ValuationEntry val, CancellationToken ct)
    {
      // Demo rule đơn giản:
      // - Issue (amount âm): Dr COGS / Cr Inventory
      // - Receipt/Adjust+ (amount dương): Dr Inventory / Cr Clearing
      var isOut = val.Qty < 0;
      var amountAbs = Math.Abs(val.Amount);

      var journal = new AccountingJournal
      {
        SourceValuationId = val.Id,
        DocNo = val.DocNo,
        PostingDate = val.PostingDate,
        Status = AccountingStatus.POSTED,
        Lines = isOut
          ? new List<AccountingLine>
            {
              new() { Account = "COGS", Debit = amountAbs, Credit = 0, Currency = "VND" },
              new() { Account = "Inventory", Debit = 0, Credit = amountAbs, Currency = "VND" }
            }
          : new List<AccountingLine>
            {
              new() { Account = "Inventory", Debit = amountAbs, Credit = 0, Currency = "VND" },
              new() { Account = "Clearing", Debit = 0, Credit = amountAbs, Currency = "VND" }
            }
      };

      await _journalRepo.InsertIfNotExistsAsync(journal, ct);
    }
  }
}
