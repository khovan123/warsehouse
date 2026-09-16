using Application.Dtos;
using Application.Interfaces;
using Contract.Responses;
using Domain.Repositories;

namespace Application.Services
{
    public class SetupsService : ISetupsService
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IBinRepository _binRepository;

        public SetupsService(
            IWarehouseRepository warehouseRepository,
            IBinRepository binRepository)
        {
            _warehouseRepository = warehouseRepository;
            _binRepository = binRepository;
        }

        public async Task<ApiResponse<SetupsDTO.Response>?> GetAllAsync(CancellationToken ct)
        {
            var warehouses = _warehouseRepository.GetAllAsync(ct);
            var bins = _binRepository.GetAllAsync(ct);
            await Task.WhenAll(warehouses, bins);
            var data = new SetupsDTO.Response(warehouses.Result, bins.Result);
            return new ApiResponse<SetupsDTO.Response>(data);
        }
    }
}