using Domain.Entities;
using Domain.Repositories;
using Infrastructure.DB;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoCollection<Category> _categories;

        public CategoryRepository(MongoDbContext context)
        {
            _categories = context.Categories;
        }
        public async Task<List<Category>?> GetAllAsync(CancellationToken ct)
        {
            var filter = Builders<Category>.Filter.Eq(c => c.IsActive, true);
            return await _categories.Find(filter).ToListAsync(ct);
        }

        public async Task<Category?> GetByIdAsync(string id, CancellationToken ct)
        {
            var exp = Builders<Category>.Filter;
            var filter = exp.And(
                exp.Eq(c => c.Id, id),
                exp.Eq(c => c.IsActive, true)
                );
            return await _categories.Find(filter).FirstOrDefaultAsync(ct);
        }
    }
}
