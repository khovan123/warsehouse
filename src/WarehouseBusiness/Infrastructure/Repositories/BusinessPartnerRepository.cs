using Domain.Entities;
using Domain.Repositories;
using Infrastructure.DB;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class BusinessPartnerRepository : IBusinessPartnerRepository
    {
        private readonly IMongoCollection<BusinessPartner> _businessPartners;

        public BusinessPartnerRepository(MongoDbContext context)
        {
            _businessPartners = context.BusinessPartnets;
        }
        public async Task<List<BusinessPartner>> GetAllAsync(CancellationToken ct)
        {
            var filter = Builders<BusinessPartner>.Filter.Empty;
            return await _businessPartners.Find(filter).ToListAsync(ct);
        }

        public async Task<BusinessPartner> GetByIdAsync(string id, CancellationToken ct)
        {
            var filter = Builders<BusinessPartner>.Filter.Eq(b => b.Id, id);
            return await _businessPartners.Find(filter).FirstOrDefaultAsync(ct);
        }
    }
}
