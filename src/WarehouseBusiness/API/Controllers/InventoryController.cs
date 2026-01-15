using API.Common;
using Application.Dtos;
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InventoryDTO.CreateRequest request, CancellationToken ct)
     => ApiBuilder.Result(await _inventoryService.CreateAsync(request, ct));

        [HttpPost("{id}/post")]
        public async Task<IActionResult> Post([FromRoute] string id, CancellationToken ct)
          => ApiBuilder.Result(await _inventoryService.PostAsync(id, ct));
    }
}
