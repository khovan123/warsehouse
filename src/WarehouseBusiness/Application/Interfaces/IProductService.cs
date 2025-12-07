using Application.DTOs;
using Contract.Responses;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<ApiResponse<ProductDTO.Response>> GetAll(CancellationToken ct);
    }
}
