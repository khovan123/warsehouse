using Domain.Entities;
using Domain.Repositories;
using Infrastructure.DB;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
  public class JournalRepository : IJournalRepository
  {
    private readonly IMongoCollection<AccountingJournal> _journal;
    public JournalRepository(MongoDbContext ctx)
    {
      _journal = ctx.Journals;
    }

    public async Task InsertIfNotExistsAsync(AccountingJournal j, CancellationToken ct)
    {
      try
      {
        await _journal.InsertOneAsync(j, cancellationToken: ct);
      }
      catch (MongoWriteException ex) when (ex.WriteError?.Category == ServerErrorCategory.DuplicateKey)
      {
        // ignore
      }
    }
  }
}