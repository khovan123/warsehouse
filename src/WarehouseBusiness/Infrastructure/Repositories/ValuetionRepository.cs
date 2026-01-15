using MongoDB.Driver;
using Domain.Entities;
using Infrastructure.DB;
using Domain.Enums;
using Domain.Repositories;

namespace Infrastructure.Repositories
{
  public class ValuationRepository : IValuationRepository
  {
    private readonly IMongoCollection<ValuationEntry> _valuationEntry;
    public ValuationRepository(MongoDbContext ctx)
    {
      _valuationEntry = ctx.Valuations;
    }

    public async Task<bool> ExistsByLedgerIdAsync(string ledgerId, CancellationToken ct)
      => await _valuationEntry.Find(x => x.LedgerId == ledgerId).AnyAsync(ct);

    public async Task InsertAsync(ValuationEntry val, CancellationToken ct)
    {
      try
      {
        await _valuationEntry.InsertOneAsync(val, cancellationToken: ct);
      }
      catch (MongoWriteException ex) when (ex.WriteError?.Category == ServerErrorCategory.DuplicateKey)
      {
        // idempotent: ignore
      }
    }

    public async Task<ValuationEntry?> GetMovementOutValuationAsync(string docNo, int lineNo, string productId, CancellationToken ct)
    {
      return await _valuationEntry
        .Find(x => x.SourceType == SourceTypeEnum.MOVEMENT && x.DocNo == docNo && x.LineNo == lineNo && x.ProductId == productId && x.Qty < 0)
        .SortByDescending(x => x.Seq)
        .FirstOrDefaultAsync(ct);
    }
  }
}
