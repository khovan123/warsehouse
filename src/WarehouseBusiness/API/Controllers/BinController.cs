using API.Common;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/v1/bins")]
    public class BinController: ControllerBase
    {
        private readonly IBinService _binService;
        
        public BinController(IBinService binService)
        {
            _binService = binService;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllBins(CancellationToken ct)
        {
            return ApiBuilder.Result(await _binService.GetAll(ct));
        }
    }
}
