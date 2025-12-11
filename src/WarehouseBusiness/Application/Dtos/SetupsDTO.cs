using Domain.Entities;

namespace Application.Dtos
{
    public record SetupsDTO
    {
        public record Response(List<Warehouse>? Warehouses = default!, List<Bin>? Bins = default!)
        {
            public List<Warehouse>? Warehouses { get; set; } = Warehouses;
            public List<Bin>? Bins { get; set; } = Bins;
        }
    }
}
