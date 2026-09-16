using API.Common;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/v1/paretos")]
    public class ParetoController : ControllerBase
    {
        private readonly IParetoService _paretoService;

        public ParetoController(IParetoService paretoService)
        {
            _paretoService = paretoService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllParetos(CancellationToken ct)
        {
            return ApiBuilder.Result(await _paretoService.GetAll(ct));
        }
    }
}
