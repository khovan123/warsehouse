using Domain.Entities;
using Domain.Repositories;
using Infrastructure.DB;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IMongoCollection<RefreshToken> _refreshToken;

        public RefreshTokenRepository(MongoDbContext context)
        {
            _refreshToken = context.RefreshTokens;
            CreateTTLIndexForAtomicDelete();
        }

        private Task<string> CreateTTLIndexForAtomicDelete() => _refreshToken.Indexes.CreateOneAsync(
            new CreateIndexModel<RefreshToken>(
                Builders<RefreshToken>.IndexKeys.Ascending(x => x.ExpiresAt),
                new CreateIndexOptions { ExpireAfter = TimeSpan.Zero }
            )
        );

        public Task CreateOneAsync(RefreshToken refreshToken, CancellationToken ct)
        {
            _refreshToken.InsertOne(refreshToken, cancellationToken: ct);
            return Task.CompletedTask;
        }

        public async Task<RefreshToken?> GetByTokenHashAsync(string token_hash, CancellationToken ct)
        {
            var exp = Builders<RefreshToken>.Filter;

            var filter = exp.And(
                exp.Eq(r => r.TokenHash, token_hash)
                );

            return await _refreshToken.Find(filter).FirstOrDefaultAsync(ct);
        }

        public Task UpdateOneAsync(RefreshToken refreshToken, CancellationToken ct)
        {
            var exp = Builders<RefreshToken>.Filter;

            var filter = exp.And(
                exp.Eq(r => r.UserId, refreshToken.UserId),
                exp.Eq(r => r.TokenHash, refreshToken.TokenHash)
            );

            var update = Builders<RefreshToken>.Update
                .Set(r => r.CreatedAt, refreshToken.CreatedAt)
                .Set(r => r.ExpiresAt, refreshToken.ExpiresAt)
                .Set(r => r.RevokedAt, refreshToken.RevokedAt)
                .Set(r => r.ReplacedByTokenHash, refreshToken.ReplacedByTokenHash)
                .Set(r => r.DeviceId, refreshToken.DeviceId);

            _refreshToken.UpdateOne(filter, update, cancellationToken: ct);
            return Task.CompletedTask;
        }
    }
}
