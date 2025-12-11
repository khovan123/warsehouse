using Domain.Entities;

namespace Application.DTOs
{
    public record BusinessPartnerDTO
    {
        public record Response(List<BusinessPartner>? BusinessPartners = default!)
        {
            public List<BusinessPartner>? BusinessPartners { get; set; } = BusinessPartners;
        }
    }
}
