using Application.Dtos;
using Contract.Responses;

namespace Application.Interfaces
{
    public interface IWarehouseService
    {
        Task<ApiResponse<WarehouseDTO.Response>?> GetAllAsync(CancellationToken ct);
    }
}
