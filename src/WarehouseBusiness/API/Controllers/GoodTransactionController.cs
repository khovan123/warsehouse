using API.Common;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [ApiController]
  [Route("/api/v1/good-transactions")]
  public class GoodTransactionController : ControllerBase
  {
    private readonly IGoodTransactionService _goodTransactionService;

    public GoodTransactionController(IGoodTransactionService goodTransactionService)
    {
      _goodTransactionService = goodTransactionService;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAllGoodTransaction(CancellationToken ct)
    {
      return ApiBuilder.Result(await _goodTransactionService.GetAll(ct));
    }
  }
}