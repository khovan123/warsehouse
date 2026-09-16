using API.Common;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/v1/businessPartners")]
    public class BusinessPartnerController : ControllerBase
    {
        private readonly IBusinessPartnerService _businessPartnerService;

        public BusinessPartnerController(IBusinessPartnerService businessPartnerService)
        {
            _businessPartnerService = businessPartnerService;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            return ApiBuilder.Result(await _businessPartnerService.GetAllAsync(ct));
        }
    }
}
