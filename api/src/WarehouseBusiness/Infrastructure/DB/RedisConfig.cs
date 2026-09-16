namespace Infrastructure.DB
{
    public class RedisConfig
    {
        public string ConnectionString { get; set; } = default!;
        public int Port { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string User { get; set; } =default!;
    }
}
