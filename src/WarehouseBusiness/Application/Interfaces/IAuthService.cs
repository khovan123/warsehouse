using Application.DTOs;
using Contract.Responses;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<Login.Response>> LoginAsync(Login.Request requestPayload, CancellationToken ct);
    }
}
