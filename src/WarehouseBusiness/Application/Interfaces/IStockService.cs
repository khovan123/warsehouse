using Application.DTOs;
using Contract.Responses;

namespace Application.Interfaces
{
    public interface IStockService
    {
        Task<ApiResponse<StockDTO.Response>> GetAllReports(CancellationToken ct);
    }
}
