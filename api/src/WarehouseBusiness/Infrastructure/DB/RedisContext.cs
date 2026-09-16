using StackExchange.Redis;

namespace Infrastructure.DB
{
    public class RedisContext
    {
        private readonly IDatabase _database;

        public RedisContext(IDatabase database)
        {
            _database = database;
        }
    }
}
