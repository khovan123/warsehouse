using Application.Dtos;
using Contract.Responses;

namespace Application.Interfaces
{
    public interface ISetupsService
    {
        Task<ApiResponse<SetupsDTO.Response>?> GetAllAsync(CancellationToken ct);
    }
}
