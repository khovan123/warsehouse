using Domain.Entities;
using Domain.Repositories;
using Infrastructure.DB;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{

  public class TransferLinkRepository : ITransferLinkRepository
  {
    private readonly IMongoCollection<TransferCostLink> _transferCostLink;
    public TransferLinkRepository(MongoDbContext ctx)
    {
      _transferCostLink = ctx.TransferLinks;
    }

    public async Task<TransferCostLink?> GetAsync(string docNo, int lineNo, string productId, CancellationToken ct)
      => await _transferCostLink.Find(x => x.DocNo == docNo && x.LineNo == lineNo && x.ProductId == productId).FirstOrDefaultAsync(ct);

    public async Task UpsertAsync(string docNo, int lineNo, string productId, decimal unitCost, string outLedgerId, CancellationToken ct)
    {
      var filter = Builders<TransferCostLink>.Filter.Where(x => x.DocNo == docNo && x.LineNo == lineNo && x.ProductId == productId);
      var update = Builders<TransferCostLink>.Update
        .SetOnInsert(x => x.DocNo, docNo)
        .SetOnInsert(x => x.LineNo, lineNo)
        .SetOnInsert(x => x.ProductId, productId)
        .Set(x => x.UnitCost, unitCost)
        .Set(x => x.OutLedgerId, outLedgerId);

      await _transferCostLink.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true }, ct);
    }
  }
}