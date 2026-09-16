using Domain.Entities;

namespace Application.Dtos
{
    public sealed record CategoryDTO
    {
        public sealed record Response(List<Category>? Categories = default!);
    }
}
