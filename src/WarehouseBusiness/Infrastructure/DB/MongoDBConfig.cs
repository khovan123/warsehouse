namespace Infrastructure.DB
{
    public class MongoDBConfig
    {
        public string ConnectionString { get; set; } = default!;
        public string DatabaseName { get; set; } = default!;
    }
}
