using Application.Dtos;
using Contract.Responses;

namespace Application.Interfaces
{
    public interface IInventoryService
    {
        Task<ApiResponse<InventoryDTO.Response>> GetAll(CancellationToken ct);
    }
}
