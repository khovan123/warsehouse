using Domain.Entities;
using Domain.Entities.Weak;
using Domain.Repositories;
using Infrastructure.DB;
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
        public async Task<List<Product>> GetAll(CancellationToken ct)
        {
            var filter = Builders<Product>.Filter.Empty;
            return await _products.Find(filter).ToListAsync(ct);
        }

        public async Task<List<ProductWithCategory>> GetAllWithCategory(CancellationToken ct)
        {
            var pipeline = _products.Aggregate()
              .Match(p => true)
              .Lookup<Product, Category, ProductWithCategory>(
                  _categories,
                  p => p.CategoryId,
                  c => c.Id,
                  pwc => pwc.Categories
              )
              .Unwind(x => x.Categories, new AggregateUnwindOptions<ProductWithCategory>
              {
                  PreserveNullAndEmptyArrays = true
              })
              .Project(x => new ProductWithCategory
              {
                  Id = x.Id,
                  AccountId = x.AccountId,
                  Label = x.Label,
                  Sku = x.Sku,
                  BaseUom = x.BaseUom,
                  Description = x.Description,
                  IsOverBook = x.IsOverBook,
                  Availability = x.Availability,
                  CategoryId = x.CategoryId,
                  Category = x.Categories != null ? x.Categories.FirstOrDefault() : null
              });
            return await pipeline.ToListAsync(ct);
        }

        public async Task<Product> GetById(string id, CancellationToken ct)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            return await _products.Find(filter).FirstOrDefaultAsync(ct);
        }
    }
}
