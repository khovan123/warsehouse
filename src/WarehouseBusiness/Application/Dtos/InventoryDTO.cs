using Domain.Entities;

namespace Application.Dtos
{
    public record InventoryDTO
    {
        public record Response(List<Inventory>? Inventories = default!)
        {
            public List<Inventory>? Inventories { get; set; } = Inventories;
        }
    }
}
