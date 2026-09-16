using API.Common;
using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [ApiController]
  [Route("api/v1/good-transactions")]
  public class GoodTransactionCommandController : ControllerBase
  {
    private readonly IGoodTransactionService _svc;

    public GoodTransactionCommandController(IGoodTransactionService svc)
    {
      _svc = svc;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] GoodTransactionDTO.CreateRequest request, CancellationToken ct)
      => ApiBuilder.Result(await _svc.CreateAsync(request, ct));

    [HttpPost("{id}/complete")]
    public async Task<IActionResult> Complete([FromRoute] string id, CancellationToken ct)
      => ApiBuilder.Result(await _svc.CompleteAsync(id, ct));

    [HttpPost("{id}/post")]
    public async Task<IActionResult> Post([FromRoute] string id, CancellationToken ct)
      => ApiBuilder.Result(await _svc.PostAsync(id, ct));
  }
}
