using Domain.Entities;

namespace Domain.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetByTokenHash(string token_hash, CancellationToken ct);

        void CreateOne(RefreshToken refreshToken, CancellationToken ct);

        void UpdateOne(RefreshToken refreshToken, CancellationToken ct);
    }
}
