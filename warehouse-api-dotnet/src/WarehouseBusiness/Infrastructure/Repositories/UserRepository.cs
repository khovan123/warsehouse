using Domain.Entities;
using Domain.Repositories;
using Infrastructure.DB;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(MongoDbContext context)
        {
            _users = context.Users;
        }
        public async Task<User?> GetByUsernameAsync(string username, CancellationToken ct = default)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Username, username);
            return await _users.Find(filter).FirstOrDefaultAsync(ct);
        }
    }
}
