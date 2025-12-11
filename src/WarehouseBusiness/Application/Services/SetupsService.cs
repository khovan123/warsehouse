using Application.Dtos;
using Application.Interfaces;
using Contract.Responses;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;

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

        public async Task<ApiResponse<SetupsDTO.Response>> GetAll(CancellationToken ct)
        {
            var warehouses = await _warehouseRepository.GetAll(ct);
            var bins = await _binRepository.GetAll(ct);
            var data = new SetupsDTO.Response(warehouses, bins);
            return new ApiResponse<SetupsDTO.Response>.SuccessBuilder(data, null, StatusCodes.Status200OK);
        }
    }
}