using Application.Dtos;
using Contract.Responses;

namespace Application.Interfaces
{
    public interface IStockService
    {
        Task<ApiResponse<StockDTO.Response>?> GetAllAsync(CancellationToken ct);
    }
}
