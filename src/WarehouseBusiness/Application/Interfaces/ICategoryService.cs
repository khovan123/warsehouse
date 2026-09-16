using Application.DTOs;
using Contract.Responses;

namespace Application.Interfaces
{
    public interface ICategoryService
    {
        Task<ApiResponse<CategoryDTO.Response>> GetAll(CancellationToken ct);
    }
}
