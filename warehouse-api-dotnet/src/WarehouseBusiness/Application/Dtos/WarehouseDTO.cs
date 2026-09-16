using Domain.Entities;

namespace Application.Dtos
{
    public sealed record WarehouseDTO
    {
        public sealed record Response(List<Warehouse>? Warehouses = default!);
    }
}
