using API.Common;
using Application.Dtos;
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
            var res = await _authService.LoginAsync(request, ct);

            if (res is ApiResponse<LoginDTO.ResponseWithRefreshToken> and not null)
            {
                var rawRefreshToken = res.Result.Data.RefreshToken ?? "";
                var data = res.Result.Data.Response;

                Response.Cookies.Append("refresh_token", rawRefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.Now.AddDays(1),
                    Path = "/api/v1/auth"
                });

                var newApiRes = new ApiResponse<LoginDTO.Response>(data, res.Result.Message, res.StatusCode);

                return ApiBuilder.Result(newApiRes);
            }

            return ApiBuilder.Result(res!);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> Refresh(CancellationToken ct)
        {
            if (!Request.Cookies.TryGetValue("refresh_token", out var rawRefreshToken) || string.IsNullOrEmpty(rawRefreshToken))
                return await Task.FromResult<IActionResult>(new UnauthorizedResult());

            var tokenHash = Hash.Sha256(rawRefreshToken);

            var res = await _authService.RefreshAccessToken(tokenHash, ct);

            if (res is ApiResponse<RefreshTokenDTO.ResponseWithRefreshToken> and not null)
            {
                var newRefreshToken = res.Result.Data.RefreshToken;
                var data = res.Result.Data.Response;

                Response.Cookies.Append("refresh_token", newRefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.Now.AddDays(1),
                    Path = "/api/v1/auth"
                });


                var newApiRes = new ApiResponse<RefreshTokenDTO.Response>(data, res.Result.Message, res.StatusCode);

                return ApiBuilder.Result(newApiRes);
            }

            return ApiBuilder.Result(res!);
        }
    }
}
