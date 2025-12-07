using Application.DTOs;
using Contract.Responses;

namespace Application.Interfaces
{
    public interface IBinService
    {
        Task<ApiResponse<BinDTO.Response>> GetAll(CancellationToken ct);
    }
}
