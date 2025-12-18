using Application.Dtos;
using Contract.Responses;

namespace Application.Interfaces
{
    public interface IBusinessPartnerService
    {
        Task<ApiResponse<BusinessPartnerDTO.Response>> GetAll(CancellationToken ct);
    }
}
