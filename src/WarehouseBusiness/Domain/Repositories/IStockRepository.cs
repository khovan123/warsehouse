using Domain.Entities;
using Domain.Entities.Weak;

namespace Domain.Repositories
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAll(CancellationToken ct);
        Task<List<StockReport>> GetAllWithDetails(CancellationToken ct);
    }
}
