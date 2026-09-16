using API.Common;
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

    [HttpGet("material-transaction")]
    public async Task<IActionResult> GetMaterialTransaction(
      [FromQuery] InventoryType? type,
      [FromQuery] string? warehouseId,
      CancellationToken ct)
    {
      return ApiBuilder.Result(await _movementService.GetMaterialTransactionAsync(type, warehouseId, ct));
    }
  }
}