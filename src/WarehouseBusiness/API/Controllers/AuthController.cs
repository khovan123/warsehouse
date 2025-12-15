using API.Common;
using Application.Dtos;
using Application.DTOs;
using Application.Helper;
using Application.Interfaces;
using Contract.Responses;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO.Request request, CancellationToken ct)
        {
            var result = await _authService.LoginAsync(request, ct);

            if (result is ApiResponse<LoginDTO.ResponseWithRefreshToken>.SuccessBuilder and not null)
            {
                ApiResponse<LoginDTO.ResponseWithRefreshToken>.SuccessBuilder successBuilder = (ApiResponse<LoginDTO.ResponseWithRefreshToken>.SuccessBuilder)result;
                var rawRefreshToken = successBuilder.Result.Data.RefreshToken;
                var data = successBuilder.Result.Data.Response;

                Response.Cookies.Append("refresh_token", rawRefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.Now.AddDays(1),
                    Path = "/api/v1/auth"
                });

                var newApiRes = new ApiResponse<LoginDTO.Response>.SuccessBuilder(data, successBuilder.Message, successBuilder.StatusCode);

                return ApiBuilder.Result(newApiRes);
            }

            return ApiBuilder.Result(result!);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(CancellationToken ct)
        {
            if (!Request.Cookies.TryGetValue("refresh_token", out var rawRefreshToken) || string.IsNullOrEmpty(rawRefreshToken))
                return await Task.FromResult<IActionResult>(new UnauthorizedResult());

            var tokenHash = Hash.Sha256(rawRefreshToken);

            var result = await _authService.RefreshAccessToken(tokenHash, ct);

            if (result is ApiResponse<RefreshTokenDTO.ResponseWithRefreshToken>.SuccessBuilder and not null)
            {
                ApiResponse<RefreshTokenDTO.ResponseWithRefreshToken>.SuccessBuilder successBuilder = (ApiResponse<RefreshTokenDTO.ResponseWithRefreshToken>.SuccessBuilder)result;
                var newRefreshToken = successBuilder.Result.Data.RefreshToken;
                var data = successBuilder.Result.Data.Response;

                Response.Cookies.Append("refresh_token", newRefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.Now.AddDays(1),
                    Path = "/api/v1/auth"
                });


                var newApiRes = new ApiResponse<RefreshTokenDTO.Response>.SuccessBuilder(data, successBuilder.Message, successBuilder.StatusCode);

                return ApiBuilder.Result(newApiRes);
            }

            return ApiBuilder.Result(result!);
        }
    }
}
