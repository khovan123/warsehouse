namespace Domain.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<List<T>?> GetAllAsync(CancellationToken ct);
        Task<T?> GetByIdAsync(string id, CancellationToken ct);
    }
}
