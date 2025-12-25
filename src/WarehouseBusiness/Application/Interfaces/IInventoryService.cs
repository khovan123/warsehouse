using Application.Dtos;
using Contract.Responses;

namespace Application.Interfaces
{
    public interface IInventoryService
    {
        Task<ApiResponse<InventoryDTO.ResponseDetails>> GetAll(CancellationToken ct);
    }
}
