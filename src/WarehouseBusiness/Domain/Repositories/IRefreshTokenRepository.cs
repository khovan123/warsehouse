using Domain.Entities;

namespace Domain.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetByTokenHashAsync(string token_hash, CancellationToken ct);

        Task CreateOneAsync(RefreshToken refreshToken, CancellationToken ct);

        Task UpdateOneAsync(RefreshToken refreshToken, CancellationToken ct);
    }
}
