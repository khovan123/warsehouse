using API.Common;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [ApiController]
  [Route("/api/v1/reservations")]
  public class ReservationController : ControllerBase
  {
    private readonly IReservationService _reservationService;

    public ReservationController(IReservationService reservationService)
    {
      _reservationService = reservationService;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAllReservations(CancellationToken ct)
    {
      return ApiBuilder.Result(await _reservationService.GetAll(ct));
    }
  }
}