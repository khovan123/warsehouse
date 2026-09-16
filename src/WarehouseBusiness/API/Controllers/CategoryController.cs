using API.Common;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            return ApiBuilder.Result(await _categoryService.GetAllAsync(ct));
        }

    }
}
