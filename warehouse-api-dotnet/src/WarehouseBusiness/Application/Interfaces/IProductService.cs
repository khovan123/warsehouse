using Application.Dtos;
using Contract.Responses;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<ApiResponse<ProductDTO.Response>?> GetAllAsync(CancellationToken ct);
    }
}
