using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/auth")]
    public class AuthController: ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login.Request request, CancellationToken ct)
        {
            var result = await _authService.LoginRequest(request, ct);
            if (result is null)
                return Unauthorized();

            return Ok(result);
        }
    }
}
