using Application.Dtos;
using Contract.Responses;

namespace Application.Interfaces
{
    public interface IBinService
    {
        Task<ApiResponse<BinDTO.Response>?> GetAllAsync(CancellationToken ct);
    }
}
