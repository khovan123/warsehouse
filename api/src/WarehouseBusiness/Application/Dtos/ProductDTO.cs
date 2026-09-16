using Domain.Entities;

namespace Application.Dtos
{
    public sealed record ProductDTO
    {
        public sealed record Response(List<Product>? Products = default!);
    }
}
