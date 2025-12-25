using Domain.Entities;
using Domain.Entities.Weak.Movement;
using Domain.Enums;
using Domain.Helpers;
using Domain.Repositories;
using Infrastructure.Constants;
using Infrastructure.DB;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
  public class MovementRepository : IMovementRepository
  {
    private readonly IMongoCollection<Movement> _movement;
    private readonly IMongoCollection<Product> _products;
    private readonly IMongoCollection<Warehouse> _warehouses;
    private readonly IMongoCollection<Bin> _bins;
    private readonly IMongoCollection<Inventory> _inventories;

    public MovementRepository(MongoDbContext context)
    {
      _movement = context.Movements;
      _products = context.Products;
      _warehouses = context.Warehouses;
      _bins = context.Bins;
      _inventories = context.Inventories;
    }

    public async Task<List<Movement>> GetAll(CancellationToken ct)
    {
      var filter = Builders<Movement>.Filter.Empty;
      return await _movement.Find(filter).ToListAsync(ct);
    }

    public async Task<Movement> GetById(string id, CancellationToken ct)
    {
      var filter = Builders<Movement>.Filter.Eq(m => m.Id, id);
      return await _movement.Find(filter).FirstOrDefaultAsync(ct);
    }

    public async Task<List<MovementReport>> GetAllWithDetails(CancellationToken ct)
    {
      var pipeline = _movement.Aggregate()
        .AppendStage<BsonDocument>(new BsonDocument("$unwind", new BsonDocument
        {
          { "path", "$lines" },
          { "preserveNullAndEmptyArrays", false }
        }))
        .AppendStage<BsonDocument>(new BsonDocument("$lookup", new BsonDocument
        {
          { "from", MongoCollections.Products },
          { "localField", "lines.product" },
          { "foreignField", "_id" },
          { "as", "products" }
        }))
        .AppendStage<BsonDocument>(new BsonDocument("$lookup", new BsonDocument
        {
          { "from", MongoCollections.Warehouses },
          { "localField", "fromWarehouse" },
          { "foreignField", "_id" },
          { "as", "fromWarehouses" }
        }))
        .AppendStage<BsonDocument>(new BsonDocument("$lookup", new BsonDocument
        {
          { "from", MongoCollections.Warehouses },
          { "localField", "toWarehouse" },
          { "foreignField", "_id" },
          { "as", "toWarehouses" }
        }))
        .AppendStage<BsonDocument>(new BsonDocument("$lookup", new BsonDocument
        {
          { "from", MongoCollections.Bins },
          { "localField", "lines.fromBin" },
          { "foreignField", "_id" },
          { "as", "fromBins" }
        }))
        .AppendStage<BsonDocument>(new BsonDocument("$lookup", new BsonDocument
        {
          { "from", MongoCollections.Inventory },
          { "localField", "lines.product" },
          { "foreignField", "productId" },
          { "as", "inventories" }
        }))
        .AppendStage<BsonDocument>(new BsonDocument("$project", new BsonDocument
        {
          { "docNo", 1 },
          { "movementDate", 1 },
          { "productEntity", new BsonDocument("$arrayElemAt", new BsonArray { "$products", 0 }) },
          { "fromWarehouse", new BsonDocument("$getField", new BsonDocument
            {
              { "field", "code" },
              { "input", new BsonDocument("$arrayElemAt", new BsonArray { "$fromWarehouses", 0 }) }
            })
          },
          { "toWarehouse", new BsonDocument("$getField", new BsonDocument
            {
              { "field", "code" },
              { "input", new BsonDocument("$arrayElemAt", new BsonArray { "$toWarehouses", 0 }) }
            })
          },
          { "binEntity", new BsonDocument("$arrayElemAt", new BsonArray { "$fromBins", 0 }) },
          { "type", new BsonDocument("$getField", new BsonDocument
            {
              { "field", "type" },
              { "input", new BsonDocument("$arrayElemAt", new BsonArray { "$inventories", 0 }) }
            })
          },
          { "qty", new BsonDocument("$cond", new BsonDocument
            {
              { "if", new BsonDocument("$in", new BsonArray 
                { 
                  new BsonDocument("$getField", new BsonDocument
                  {
                    { "field", "type" },
                    { "input", new BsonDocument("$arrayElemAt", new BsonArray { "$inventories", 0 }) }
                  }),
                  new BsonArray { "Movement", "Shipment" } 
                }) 
              },
              { "then", new BsonDocument("$multiply", new BsonArray { "$lines.qty", -1 }) },
              { "else", "$lines.qty" }
            })
          }
        }))
        .As<MovementReport>();

      return await pipeline.ToListAsync(ct);
    }

    public async Task<List<MovementSummary>> GetSummary(SummaryPeriod period, CancellationToken ct)
    {
      var dateFormat = DateFormat.GetDateFormat(period);

      var pipeline = _movement.Aggregate()
        .AppendStage<BsonDocument>(new BsonDocument("$unwind", new BsonDocument
        {
          { "path", "$lines" },
          { "preserveNullAndEmptyArrays", false }
        }))
        .AppendStage<BsonDocument>(new BsonDocument("$lookup", new BsonDocument
        {
          { "from", MongoCollections.Inventory },
          { "localField", "lines.product" },
          { "foreignField", "productId" },
          { "as", "inventories" }
        }))
        .AppendStage<BsonDocument>(new BsonDocument("$project", new BsonDocument
        {
          { "movementDate", 1 },
          { "calculatedQty", new BsonDocument("$cond", new BsonDocument
            {
              { "if", new BsonDocument("$in", new BsonArray 
                { 
                  new BsonDocument("$getField", new BsonDocument
                  {
                    { "field", "type" },
                    { "input", new BsonDocument("$arrayElemAt", new BsonArray { "$inventories", 0 }) }
                  }),
                  new BsonArray { "Movement", "Shipment" } 
                }) 
              },
              { "then", new BsonDocument("$multiply", new BsonArray { "$lines.qty", -1 }) },
              { "else", "$lines.qty" }
            })
          }
        }))
        .AppendStage<BsonDocument>(new BsonDocument("$group", new BsonDocument
        {
          { "_id", new BsonDocument("$dateToString", new BsonDocument
            {
              { "format", dateFormat },
              { "date", new BsonDocument("$toDate", "$movementDate") }
            })
          },
          { "negativeQty", new BsonDocument("$sum", new BsonDocument("$cond", new BsonArray 
            { 
              new BsonDocument("$lt", new BsonArray { "$calculatedQty", 0 }),
              "$calculatedQty",
              0 
            }))
          },
          { "positiveQty", new BsonDocument("$sum", new BsonDocument("$cond", new BsonArray 
            { 
              new BsonDocument("$gt", new BsonArray { "$calculatedQty", 0 }),
              "$calculatedQty",
              0 
            }))
          },
          { "totalQty", new BsonDocument("$sum", "$calculatedQty") }
        }))
        .AppendStage<BsonDocument>(new BsonDocument("$project", new BsonDocument
        {
          { "_id", 0 },
          { "period", "$_id" },
          { "negativeQty", 1 },
          { "positiveQty", 1 },
          { "totalQty", 1 }
        }))
        .AppendStage<BsonDocument>(new BsonDocument("$sort", new BsonDocument("period", 1)))
        .As<MovementSummary>();

      return await pipeline.ToListAsync(ct);
    }
  }
}