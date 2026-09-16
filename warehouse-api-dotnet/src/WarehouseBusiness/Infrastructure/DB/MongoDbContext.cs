using Domain.Entities;
using Infrastructure.Constants;
using MongoDB.Driver;
namespace Infrastructure.DB
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IMongoDatabase mongoDatabase)
        {
            _database = mongoDatabase;
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>(MongoCollections.Users);

        public IMongoCollection<Category> Categories => _database.GetCollection<Category>(MongoCollections.Categories);

        public IMongoCollection<Warehouse> Warehouses => _database.GetCollection<Warehouse>(MongoCollections.Warehouses);

        public IMongoCollection<Bin> Bins => _database.GetCollection<Bin>(MongoCollections.Bins);

        public IMongoCollection<Product> Products => _database.GetCollection<Product>(MongoCollections.Products);

        public IMongoCollection<BusinessPartner> BusinessPartnets => _database.GetCollection<BusinessPartner>(MongoCollections.BusinessPartners);

        public IMongoCollection<Inventory> Inventories => _database.GetCollection<Inventory>(MongoCollections.Inventory);
        public IMongoCollection<RefreshToken> RefreshTokens => _database.GetCollection<RefreshToken>(MongoCollections.RefreshTokens);
        public IMongoCollection<Stock> Stocks => _database.GetCollection<Stock>(MongoCollections.Stocks);
        public IMongoCollection<Movement> Movements => _database.GetCollection<Movement>(MongoCollections.Movements);
        public IMongoCollection<GoodTransaction> GoodTransactions => _database.GetCollection<GoodTransaction>(MongoCollections.GoodTransactions);
        public IMongoCollection<Reservation> Reservations => _database.GetCollection<Reservation>(MongoCollections.Reservations);
        public IMongoCollection<Pareto> Paretos => _database.GetCollection<Pareto>(MongoCollections.Paretos);
        public IMongoCollection<InventoryLedger> InventoryLedgers => _database.GetCollection<InventoryLedger>("inventory_ledger");
        public IMongoCollection<CostState> CostStates => _database.GetCollection<CostState>("cost_state");
        public IMongoCollection<ValuationEntry> Valuations => _database.GetCollection<ValuationEntry>("valuation_entry");
        public IMongoCollection<AccountingJournal> Journals => _database.GetCollection<AccountingJournal>("accounting_journal");
        public IMongoCollection<OutboxEvent> Outbox => _database.GetCollection<OutboxEvent>("outbox");
        public IMongoCollection<TransferCostLink> TransferLinks => _database.GetCollection<TransferCostLink>("transfer_cost_link");
        public IMongoCollection<Counter> Counters => _database.GetCollection<Counter>("counters");
        public IMongoCollection<CostingCheckpoint> CostingCheckpoints => _database.GetCollection<CostingCheckpoint>("costing_checkpoint");

    }
}
