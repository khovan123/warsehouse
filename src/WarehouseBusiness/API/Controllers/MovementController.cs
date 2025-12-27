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
    public async Task<IActionResult> GetAllMovements(CancellationToken ct)
    {
      return ApiBuilder.Result(await _movementService.GetAll(ct));
    }
    
    [HttpGet("report")]
    public async Task<IActionResult> GetMovementReport([FromQuery] InventoryType? type, CancellationToken ct)
    {
      return ApiBuilder.Result(await _movementService.GetAsReport(type, ct));
    }
    
    [HttpGet("summary")]
    public async Task<IActionResult> GetMovementSummary([FromQuery] SummaryPeriod period, CancellationToken ct)
    {
      return ApiBuilder.Result(await _movementService.GetAsSummary(period, ct));
    }
  }
}