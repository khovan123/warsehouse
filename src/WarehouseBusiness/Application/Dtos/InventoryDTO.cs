using Domain.Entities;
using Domain.Entities.Weak;

namespace Application.Dtos
{
    public record InventoryDTO
    {
        // public record Response(List<Inventory>? Inventories = default!)
        // {
        //     public List<Inventory>? Inventories { get; set; } = Inventories;
        // }

        public record ResponseDetails(List<InventoryDetails>? Inventories = default!)
        {
            public List<InventoryDetails>? Inventories { get; set; } = Inventories;
        }
    }
}
