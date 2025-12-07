using Domain.Entities;
using Domain.Repositories;
using Infrastructure.DB;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _products;

        public ProductRepository(MongoDbContext context)
        {
            _products = context.Products;
        }
        public async Task<List<Product>> GetAll(CancellationToken ct)
        {
            var filter = Builders<Product>.Filter.Empty;
            return await _products.Find(filter).ToListAsync(ct);
        }

        public async Task<Product> GetById(string id, CancellationToken ct)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            return await _products.Find(filter).FirstOrDefaultAsync(ct);
        }
    }
}
