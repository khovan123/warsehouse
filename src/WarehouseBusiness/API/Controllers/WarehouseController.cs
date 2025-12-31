using API.Common;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/v1/warehouses")]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            return ApiBuilder.Result(await _warehouseService.GetAllAsync(ct));
        }
    }
}
