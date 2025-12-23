using Domain.Entities;
using Domain.Entities.Weak;

namespace Domain.Repositories
{
    public interface IParetoRepository: IBaseRepository<Pareto>
    {
        Task<List<ParetoReport>> GetAllWithDetails(CancellationToken ct);
    }
}
