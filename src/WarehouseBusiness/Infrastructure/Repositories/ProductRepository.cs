using Domain.Entities;
using Domain.Entities.Weak;
using Domain.Repositories;
using Infrastructure.Constants;
using Infrastructure.DB;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _products;
        private readonly IMongoCollection<Category> _categories;

        public ProductRepository(MongoDbContext context)
        {
            _products = context.Products;
            _categories = context.Categories;
        }
        public async Task<List<Product>> GetAllAsync(CancellationToken ct)
        {
            var filter = Builders<Product>.Filter.Empty;
            return await _products.Find(filter).ToListAsync(ct);
        }

        public async Task<List<ProductDetails>> GetAllWithCategoryAsync(CancellationToken ct)
        {
            var pipeline = _products.Aggregate()
              .Match(p => true)
              .AppendStage<BsonDocument>(new BsonDocument("$lookup", new BsonDocument {
                    { "from", MongoCollections.Categories },
                    { "localField", "categoryId" },
                    { "foreignField", "_id" },
                    { "as", "tmp_categories" }
                }))
              .AppendStage<BsonDocument>(new BsonDocument("$unwind", new BsonDocument
              {
                    {"path", "tmp_categories"},
                    {"preserveNullAndEmptyArrays", true}
              }))
              .AppendStage<BsonDocument>(new BsonDocument("$addFields", new BsonDocument
              {
                    {"catogory", "$tmp_categories"}
              }))
              .AppendStage<BsonDocument>(new BsonDocument("$project", new BsonDocument
              {
                    {"tmp_categories", 0}
              }))
              .As<ProductDetails>();

            return await pipeline.ToListAsync(ct);
        }

        public async Task<Product> GetByIdAsync(string id, CancellationToken ct)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            return await _products.Find(filter).FirstOrDefaultAsync(ct);
        }
    }
}
