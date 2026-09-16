using Application.Dtos;
using Application.Interfaces;
using Contract.Responses;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;

        public StockService(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<ApiResponse<StockDTO.Response>?> GetAllAsync(CancellationToken ct)
        {
            var stockReports = await _stockRepository.GetAllAsync(ct);

            var data = new StockDTO.Response(stockReports);
            return new ApiResponse<StockDTO.Response>(data);
        }
    }
}
