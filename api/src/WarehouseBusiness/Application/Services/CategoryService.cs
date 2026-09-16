using Application.Dtos;
using Application.Interfaces;
using Contract.Responses;
using Domain.Repositories;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<ApiResponse<CategoryDTO.Response>?> GetAllAsync(CancellationToken ct)
        {
            var categories = await _categoryRepository.GetAllAsync(ct);
            var data = new CategoryDTO.Response(categories);
            return new ApiResponse<CategoryDTO.Response>(data);
        }
    }
}
