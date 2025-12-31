using Domain.Entities;
using Domain.Entities.Weak;
using Domain.Repositories;
using Infrastructure.Constants;
using Infrastructure.DB;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class ParetoRepository : IParetoRepository
    {
        private readonly IMongoCollection<Pareto> _paretos;
        private readonly IMongoCollection<Product> _products;
        private readonly IMongoCollection<Category> _categories;

        public ParetoRepository(MongoDbContext context)
        {
            _paretos = context.Paretos;
            _products = context.Products;
            _categories = context.Categories;
        }

        public async Task<List<ParetoDetails>> GetAllAsync(CancellationToken ct)
        {
            var pipeline = _paretos.Aggregate()
                .Match(p => true)
                .AppendStage<BsonDocument>(new BsonDocument("$lookup", new BsonDocument
                {
                    { "from", MongoCollections.Products },
                    { "localField", "productId" },
                    { "foreignField", "_id" },
                    { "as", "products" }
                }))
                .AppendStage<BsonDocument>(new BsonDocument("$lookup", new BsonDocument
                {
                    { "from", MongoCollections.Categories },
                    { "localField", "categoryId" },
                    { "foreignField", "_id" },
                    { "as", "categories" }
                }))
                .AppendStage<BsonDocument>(new BsonDocument("$project", new BsonDocument
                {
                    { "productId", 1 },
                    { "categoryId", 1 },
                    { "annualConsumption", 1 },
                    { "value", 1 },
                    { "tag", 1 },
                    { "product", new BsonDocument("$arrayElemAt", new BsonArray { "$products", 0 }) },
                    { "category", new BsonDocument("$arrayElemAt", new BsonArray { "$categories", 0 }) }
                }))
                .As<ParetoDetails>();

            return await pipeline.ToListAsync(ct);
        }

        public async Task<ParetoDetails> GetByIdAsync(string id, CancellationToken ct)
        {
            var pipeline = _paretos.Aggregate()
               .Match(p => string.Equals(p.Id, id, StringComparison.OrdinalIgnoreCase))
               .AppendStage<BsonDocument>(new BsonDocument("$lookup", new BsonDocument
               {
                    { "from", MongoCollections.Products },
                    { "localField", "productId" },
                    { "foreignField", "_id" },
                    { "as", "products" }
               }))
               .AppendStage<BsonDocument>(new BsonDocument("$lookup", new BsonDocument
               {
                    { "from", MongoCollections.Categories },
                    { "localField", "categoryId" },
                    { "foreignField", "_id" },
                    { "as", "categories" }
               }))
               .AppendStage<BsonDocument>(new BsonDocument("$project", new BsonDocument
               {
                    { "productId", 1 },
                    { "categoryId", 1 },
                    { "annualConsumption", 1 },
                    { "value", 1 },
                    { "tag", 1 },
                    { "product", new BsonDocument("$arrayElemAt", new BsonArray { "$products", 0 }) },
                    { "category", new BsonDocument("$arrayElemAt", new BsonArray { "$categories", 0 }) }
               }))
               .As<ParetoDetails>();

            return await pipeline.FirstOrDefaultAsync(ct);
        }
    }
}
