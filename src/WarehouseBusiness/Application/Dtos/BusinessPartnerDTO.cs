using Domain.Entities;

namespace Application.Dtos
{
    public record BusinessPartnerDTO
    {
        public record Response(List<BusinessPartner>? BusinessPartners = default!)
        {
            public List<BusinessPartner>? BusinessPartners { get; set; } = BusinessPartners;
        }
    }
}
