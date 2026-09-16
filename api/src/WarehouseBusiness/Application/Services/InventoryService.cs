using Application.Dtos;
using Application.Interfaces;
using Contract.Responses;
using Domain.Entities;
using Domain.Repositories;
using MongoDB.Bson;

namespace Application.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IDocumentPostingService _posting;

        public InventoryService(IInventoryRepository inventoryRepository, IProductRepository productRepository, IDocumentPostingService posting)
        {
            _inventoryRepository = inventoryRepository;
            _productRepository = productRepository;
            _posting = posting;
        }

        public async Task<ApiResponse<InventoryDTO.ResponseDetails>?> GetAllAsync(CancellationToken ct)
        {
            var inventories = await _inventoryRepository.GetAllAsync(ct);
            var data = new InventoryDTO.ResponseDetails(inventories);
            return new ApiResponse<InventoryDTO.ResponseDetails>(data);
        }

        public async Task<ApiResponse<InventoryDTO.CreateResponse>?> CreateAsync(InventoryDTO.CreateRequest request, CancellationToken ct)
        {
            var inv = new Inventory
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Document = request.Document,
                Line = request.Line,
                Type = request.Type,
                PostedAt = request.PostedAt,
                ProductId = request.ProductId,
                WarehouseId = request.WarehouseId,
                BinId = request.BinId,
                Qty = request.Qty,
                Uom = request.Uom,
                UnitCost = request.UnitCost,
                BpartnerId = request.BpartnerId
            };

            await _inventoryRepository.InsertAsync(inv, ct);
            return new ApiResponse<InventoryDTO.CreateResponse>(new InventoryDTO.CreateResponse(inv.Id));
        }

        public async Task<ApiResponse<InventoryDTO.PostResponse>?> PostAsync(string id, CancellationToken ct)
        {
            var inv = await _inventoryRepository.GetRawByIdAsync(id, ct);
            if (inv == null)
                return new ApiResponse<InventoryDTO.PostResponse>(new InventoryDTO.PostResponse(id, "Inventory not found."), "Inventory not found", 404);

            await _posting.PostInventoryAsync(inv, ct);

            return new ApiResponse<InventoryDTO.PostResponse>(new InventoryDTO.PostResponse(id, "Posted"));
        }
    }
}
