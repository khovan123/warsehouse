using Domain.Entities;

namespace Application.DTOs
{
    public record ProductDTO
    {
        public record Response(List<Product>? Products = default!)
        {
            public List<Product>? Products { get; set; } = Products;
        }
    }
}
