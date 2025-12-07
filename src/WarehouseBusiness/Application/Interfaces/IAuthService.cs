using Application.DTOs;
using Contract.Responses;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<LoginDTO.Response>> LoginAsync(LoginDTO.Request requestPayload, CancellationToken ct);
    }
}
