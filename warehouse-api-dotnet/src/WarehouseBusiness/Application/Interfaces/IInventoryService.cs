using Application.Dtos;
using Contract.Responses;

namespace Application.Interfaces
{
    public interface IInventoryService
    {
        Task<ApiResponse<InventoryDTO.ResponseDetails>?> GetAllAsync(CancellationToken ct);

        Task<ApiResponse<InventoryDTO.CreateResponse>?> CreateAsync(InventoryDTO.CreateRequest request, CancellationToken ct);
        Task<ApiResponse<InventoryDTO.PostResponse>?> PostAsync(string id, CancellationToken ct);

    }
}
