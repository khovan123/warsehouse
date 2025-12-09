using Domain.Entities;

namespace Domain.Repositories
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAll(CancellationToken ct);
    }
}
