using API.Common;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/v1/inventories")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            return ApiBuilder.Result(await _inventoryService.GetAllAsync(ct));
        }
    }
}
