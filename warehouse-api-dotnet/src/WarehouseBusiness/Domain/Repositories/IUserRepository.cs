using Domain.Entities;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username, CancellationToken ct = default);
    }
}
