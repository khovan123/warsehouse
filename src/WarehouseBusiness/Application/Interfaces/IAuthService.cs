using Application.Dtos;
using Contract.Responses;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<LoginDTO.ResponseWithRefreshToken>?> LoginAsync(LoginDTO.Request requestPayload, CancellationToken ct);

        Task<ApiResponse<RefreshTokenDTO.ResponseWithRefreshToken>?> RefreshAccessTokenAsync(string token_hash, CancellationToken ct);
    }
}
