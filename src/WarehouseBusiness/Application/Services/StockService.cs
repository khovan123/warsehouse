using Application.DTOs;
using Application.Interfaces;
using Contract.Responses;
using Domain.Entities.Weak;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        private readonly IProductRepository _productRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IBinRepository _binRepository;
        private readonly ICategoryRepository _categoryRepository;

        public StockService(
            IStockRepository stockRepository,
            IProductRepository productRepository,
            IWarehouseRepository warehouseRepository,
            IBinRepository binRepository,
            ICategoryRepository categoryRepository)
        {
            _stockRepository = stockRepository;
            _productRepository = productRepository;
            _warehouseRepository = warehouseRepository;
            _binRepository = binRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<ApiResponse<StockDTO.Response>> GetAllReports(CancellationToken ct)
        {
            var stocks = await _stockRepository.GetAll(ct);
            var products = await _productRepository.GetAll(ct);
            var warehouses = await _warehouseRepository.GetAll(ct);
            var bins = await _binRepository.GetAll(ct);
            var categories = await _categoryRepository.GetAll(ct);

            var stockReports = stocks.Select(stock =>
            {
                var stockReport = new StockReport
                {
                    Id = stock.Id,
                    ProductId = stock.ProductId,
                    WarehouseId = stock.WarehouseId,
                    BinId = stock.BinId,
                    CategoryId = stock.CategoryId,
                    OnHand = stock.OnHand,
                    Reserved = stock.Reserved,
                    Available = stock.Available,
                    AverageCost = stock.AverageCost,
                    InventoryValue = stock.InventoryValue,
                    ProductEntity = products.FirstOrDefault(p => p.Id == stock.ProductId),
                    WarehouseEntity = warehouses.FirstOrDefault(w => w.Id == stock.WarehouseId),
                    BinEntity = bins.FirstOrDefault(b => b.Id == stock.BinId),
                    CategoryEntity = categories.FirstOrDefault(c => c.Id == stock.CategoryId)
                };
                return stockReport;
            }).ToList();

            var data = new StockDTO.Response(stockReports);
            return new ApiResponse<StockDTO.Response>.SuccessBuilder(data, "Get resources successfully!", StatusCodes.Status200OK);
        }
    }
}
