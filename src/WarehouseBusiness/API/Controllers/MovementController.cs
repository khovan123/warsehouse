using API.Common;
using Application.Dtos;
using Application.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [ApiController]
  [Route("/api/v1/movements")]
  public class MovementController : ControllerBase
  {
    private readonly IMovementService _movementService;

    public MovementController(IMovementService movementService)
    {
      _movementService = movementService;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
      return ApiBuilder.Result(await _movementService.GetAllAsync(ct));
    }

    [HttpGet("report")]
    public async Task<IActionResult> GetAllWithDetails(
      [FromQuery] InventoryType? type,
      [FromQuery] SummaryPeriod period,
      CancellationToken ct = default)
    {
      return ApiBuilder.Result(await _movementService.GetAllWithDetailsAsync(type, period, ct));
    }

    [HttpGet("summary")]
    public async Task<IActionResult> GetSummary([FromQuery] SummaryPeriod period, CancellationToken ct)
    {
      return ApiBuilder.Result(await _movementService.GetSummaryAsync(period, ct));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MovementDTO.CreateRequest request, CancellationToken ct)
      => ApiBuilder.Result(await _movementService.CreateAsync(request, ct));

    [HttpPost("{id}/complete")]
    public async Task<IActionResult> Complete([FromRoute] string id, CancellationToken ct)
      => ApiBuilder.Result(await _movementService.CompleteAsync(id, ct));

    [HttpPost("{id}/post")]
    public async Task<IActionResult> Post([FromRoute] string id, CancellationToken ct)
      => ApiBuilder.Result(await _movementService.PostAsync(id, ct));
  }
}