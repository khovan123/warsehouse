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
  public class ReservationRepository : IReservationRepository
  {
    private readonly IMongoCollection<Reservation> _reservation;

    public ReservationRepository(MongoDbContext context)
    {
      _reservation = context.Reservations;
    }
    public async Task<List<ReservationDetails>> GetAllAsync(CancellationToken ct)
    {
      var pipeline = new MongoAggregationPipeline<Reservation>(_reservation)
        .Match(r => true)
        .LookupAndUnwind(MongoCollections.Products, "productId", $"tmp_{MongoCollections.Products}")
        .LookupAndUnwind(MongoCollections.Warehouses, "warehouseId", $"tmp_{MongoCollections.Warehouses}")
        .SetFields(new BsonDocument
        {
          {"productName",$"$tmp_{MongoCollections.Products}.description"},
          {"warehouseName",$"$tmp_{MongoCollections.Warehouses}.name"}
        })
        .Unset(new BsonArray { $"tmp_{MongoCollections.Products}", $"tmp_{MongoCollections.Warehouses}" })
        .As<ReservationDetails>();
      return await pipeline.ToListAsync(ct);
    }

    public async Task<ReservationDetails> GetByIdAsync(string id, CancellationToken ct)
    {
      var pipeline = new MongoAggregationPipeline<Reservation>(_reservation)
       .Match(r => string.Equals(r.Id, id, StringComparison.OrdinalIgnoreCase))
       .LookupAndUnwind(MongoCollections.Products, "productId", $"tmp_{MongoCollections.Products}")
       .LookupAndUnwind(MongoCollections.Warehouses, "warehouseId", $"tmp_{MongoCollections.Warehouses}")
       .SetFields(new BsonDocument
       {
          {"productName",$"$tmp_{MongoCollections.Products}"},
          {"warehouseName",$"$tmp_{MongoCollections.Warehouses}"}
       })
       .Unset(new BsonArray { $"tmp_{MongoCollections.Products}", $"tmp_{MongoCollections.Warehouses}" })
       .As<ReservationDetails>();
      return await pipeline.FirstOrDefaultAsync(ct);
    }
  }
}