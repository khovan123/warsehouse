using Application.DTOs;
using Contract.Responses;

namespace Application.Interfaces
{
    public interface IStockService
    {
        Task<ApiResponse<StockDTO.Response>> GetAll(CancellationToken ct = default);
    }
}
