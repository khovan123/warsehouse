using Domain.Entities;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByUsername (string username, CancellationToken ct = default);
    }
}
