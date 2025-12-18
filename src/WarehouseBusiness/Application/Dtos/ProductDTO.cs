using Domain.Entities;

namespace Application.Dtos
{
    public record ProductDTO
    {
        public record Response(List<Product>? Products = default!)
        {
            public List<Product>? Products { get; set; } = Products;
        }
    }
}
