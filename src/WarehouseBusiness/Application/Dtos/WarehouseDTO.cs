using Domain.Entities;

namespace Application.Dtos
{
    public record WarehouseDTO
    {
        public record Response(List<Warehouse>? Warehouses = default!)
        {
            public List<Warehouse>? Warehouses { get; set; } = Warehouses;
        }
    }
}
