using Application.DTOs;
using Contract.Responses;

namespace Application.Interfaces
{
    public interface IParetoService
    {
        Task<ApiResponse<ParetoDTO.Response>?> GetAllAsync(CancellationToken ct);
    }
}
