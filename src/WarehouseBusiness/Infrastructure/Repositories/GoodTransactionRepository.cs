using Domain.Entities;
using Domain.Entities.Weak;
using Domain.Repositories;
using Infrastructure.Constants;
using Infrastructure.DB;
using Infrastructure.Helpers;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
  public class GoodTransactionRepository : IGoodTransactionRepository
  {
    private readonly IMongoCollection<GoodTransaction> _goodTransaction;

    public GoodTransactionRepository(MongoDbContext context)
    {
      _goodTransaction = context.GoodTransactions;
    }

    public async Task<List<GoodTransactionDetails>?> GetAllAsync(CancellationToken ct)
    {
      var pipeline = new MongoAggregationPipeline<GoodTransaction>(_goodTransaction)
         .Match(m => true)
         .Unwind("$lines", false)
         .LookupAndUnwind(MongoCollections.Warehouses, "warehouseId", $"tmp_{MongoCollections.Warehouses}")
         .LookupAndUnwind(MongoCollections.Products, "lines.productId", $"tmp_{MongoCollections.Products}")
         .LookupAndUnwind(MongoCollections.Bins, "lines.binId", $"tmp_{MongoCollections.Bins}")
         .SetFields(new BsonDocument
         {
          {"warehouseName",$"$tmp_{MongoCollections.Warehouses}.name"},
          {"lines.productName",$"$tmp_{MongoCollections.Products}.description"},
          {"lines.binName",$"$tmp_{MongoCollections.Bins}.code"},
         })
         .Unset(new BsonArray {
          $"tmp_{MongoCollections.Warehouses}",
          $"tmp_{MongoCollections.Products}",
          $"tmp_{MongoCollections.Bins}",
         })
         .Group(new BsonDocument
         {
          { "_id", "$_id" },
          { "docNo", new BsonDocument("$first", "$docNo") },
          { "warehouseId", new BsonDocument("$first", "$warehouseId") },
          { "warehouseName", new BsonDocument("$first", "$warehouseName") },
          { "countDate", new BsonDocument("$first", "$countDate") },
          { "description", new BsonDocument("$first", "$description") },
          { "status", new BsonDocument("$first", "$status") },
          { "createdBy", new BsonDocument("$first", "$createdBy") },
          { "lines", new BsonDocument("$push", "$lines") }
         })
         .As<GoodTransactionDetails>();
      return await pipeline.ToListAsync(ct);
    }

    public async Task<GoodTransactionDetails?> GetByIdAsync(string id, CancellationToken ct)
    {
      var pipeline = new MongoAggregationPipeline<GoodTransaction>(_goodTransaction)
         .Match(m => string.Equals(m.Id, id, StringComparison.OrdinalIgnoreCase))
         .Unwind("$lines", false)
         .LookupAndUnwind(MongoCollections.Warehouses, "warehouseId", $"tmp_{MongoCollections.Warehouses}")
         .LookupAndUnwind(MongoCollections.Products, "lines.productId", $"tmp_{MongoCollections.Products}")
         .LookupAndUnwind(MongoCollections.Bins, "lines.binId", $"tmp_{MongoCollections.Bins}")
         .SetFields(new BsonDocument
         {
          {"warehouseName",$"$tmp_{MongoCollections.Warehouses}.name"},
          {"lines.productName",$"$tmp_{MongoCollections.Products}.description"},
          {"lines.binName",$"$tmp_{MongoCollections.Bins}.code"},
         })
         .Unset(new BsonArray {
          $"tmp_{MongoCollections.Warehouses}",
          $"tmp_{MongoCollections.Products}",
          $"tmp_{MongoCollections.Bins}",
         })
         .Group(new BsonDocument
         {
          { "_id", "$_id" },
          { "docNo", new BsonDocument("$first", "$docNo") },
          { "warehouseId", new BsonDocument("$first", "$warehouseId") },
          { "warehouseName", new BsonDocument("$first", "$warehouseName") },
          { "countDate", new BsonDocument("$first", "$countDate") },
          { "description", new BsonDocument("$first", "$description") },
          { "status", new BsonDocument("$first", "$status") },
          { "createdBy", new BsonDocument("$first", "$createdBy") },
          { "lines", new BsonDocument("$push", "$lines") }
         })
         .As<GoodTransactionDetails>();
      return await pipeline.FirstOrDefaultAsync(ct);
    }
  }
}