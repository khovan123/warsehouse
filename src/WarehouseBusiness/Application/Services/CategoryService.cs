using Application.Dtos;
using Application.Interfaces;
using Contract.Responses;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<ApiResponse<CategoryDTO.Response>> GetAll(CancellationToken ct)
        {
            var categories = await _categoryRepository.GetAll(ct);
            var data = new CategoryDTO.Response(categories);
            return new ApiResponse<CategoryDTO.Response>(data);
        }
    }
}
