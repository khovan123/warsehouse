using Domain.Entities;

namespace Application.DTOs
{
    public record WarehouseDTO
    {
        public record Response(List<Warehouse>? Warehouses = default!)
        {
            public List<Warehouse>? Warehouses { get; set; } = Warehouses;
        }
    }
}
