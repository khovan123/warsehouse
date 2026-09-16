using Domain.Entities;
using System.Text.Json.Serialization;

namespace Application.Dtos
{
    public sealed record ProductDTO
    {
        public sealed record Response(List<Product>? Products = default!);
    }
}
