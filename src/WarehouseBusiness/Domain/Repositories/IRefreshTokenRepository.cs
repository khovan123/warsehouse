using Domain.Entities;

namespace Domain.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetByTokenHashAsync(string token_hash, CancellationToken ct);

        void CreateOneAsync(RefreshToken refreshToken, CancellationToken ct);

        void UpdateOneAsync(RefreshToken refreshToken, CancellationToken ct);
    }
}
