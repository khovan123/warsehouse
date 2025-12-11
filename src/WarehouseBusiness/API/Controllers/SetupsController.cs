using API.Common;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/v1/setups")]
    public class SetupsController:ControllerBase
    {
        private readonly ISetupsService _setupsService;

        public SetupsController(ISetupsService setupsService)
        {
            _setupsService = setupsService;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            return ApiBuilder.Result(await _setupsService.GetAll(ct));
        }
    }
}
