using Application.Dtos;
using Application.Interfaces;
using Contract.Responses;
using Domain.Repositories;

namespace Application.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<ApiResponse<InventoryDTO.ResponseDetails>?> GetAllAsync(CancellationToken ct)
        {
            var inventories = await _inventoryRepository.GetAllAsync(ct);
            var data = new InventoryDTO.ResponseDetails(inventories);
            return new ApiResponse<InventoryDTO.ResponseDetails>(data);
        }
    }
}
