using Application.Dtos;
using Contract.Responses;

namespace Application.Interfaces
{
    public interface ICategoryService
    {
        Task<ApiResponse<CategoryDTO.Response>> GetAll(CancellationToken ct);
    }
}
