using Application.Dtos;
using Application.Interfaces;
using Contract.Responses;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<ApiResponse<InventoryDTO.Response>> GetAll(CancellationToken ct)
        {
            var inventories = await _inventoryRepository.GetAll(ct);
            var data = new InventoryDTO.Response(inventories);
            return new ApiResponse<InventoryDTO.Response>.SuccessBuilder(data, null, StatusCodes.Status200OK);
        }
    }
}
