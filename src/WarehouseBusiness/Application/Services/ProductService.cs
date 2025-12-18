using Application.Dtos;
using Application.Interfaces;
using Contract.Responses;
using Domain.Repositories;
using Infrastructure.DB;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ApiResponse<ProductDTO.Response>> GetAll(CancellationToken ct)
        {
            var products = await _productRepository.GetAll(ct);

            var data = new ProductDTO.Response(products);

            return new ApiResponse<ProductDTO.Response>.SuccessBuilder(data, "Get resources successfully!", StatusCodes.Status200OK);
        }
    }
}
