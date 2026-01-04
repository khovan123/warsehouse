using Domain.Entities;

namespace Domain.Repositories
{
  public interface IJournalRepository
  {
    Task InsertIfNotExistsAsync(AccountingJournal j, CancellationToken ct);
  }
}