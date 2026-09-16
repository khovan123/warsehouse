using Application.Dtos;
using Application.Interfaces;
using Contract.Responses;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository _warehouseRepository;

        public WarehouseService(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        public async Task<ApiResponse<WarehouseDTO.Response>?> GetAllAsync(CancellationToken ct)
        {
            var warehouses = await _warehouseRepository.GetAllAsync(ct);
            var data = new WarehouseDTO.Response(warehouses);
            return new ApiResponse<WarehouseDTO.Response>(data);
        }
    }
}
