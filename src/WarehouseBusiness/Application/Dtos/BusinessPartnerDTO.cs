using Domain.Entities;

namespace Application.Dtos
{
    public sealed record BusinessPartnerDTO
    {
        public sealed record Response(List<BusinessPartner>? BusinessPartners = default!);
    }
}
