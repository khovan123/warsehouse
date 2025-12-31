using Domain.Entities;
using Domain.Entities.Weak;

namespace Domain.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<List<ProductDetails>> GetAllWithCategoryAsync(CancellationToken ct);
    }
}
