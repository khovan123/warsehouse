using Application.DTOs;
using Contract.Responses;

namespace Application.Interfaces
{
    public interface IWarehouseService
    {
        Task<ApiResponse<WarehouseDTO.Response>> GetAll(CancellationToken ct);
    }
}
