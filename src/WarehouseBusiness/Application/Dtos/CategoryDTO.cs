using Domain.Entities;

namespace Application.DTOs
{
    public record CategoryDTO
    {
        public record Response(List<Category>? Categories = default!)
        {
            public List<Category>? Categories {  get; set; } = Categories;
        }
    }
}
