using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/v1/health")]
    public class CheckHealthController : Controller
    {
        private readonly IHealthService _healthService;

        public CheckHealthController(IHealthService healthService)
        {
            _healthService = healthService;
        }

        [HttpGet]
        public async Task<string> CheckHealth()
        {
            return await _healthService.CheckHealth();
        }
    }
}
