using Domain.Entities;
using Domain.Entities.Weak;

namespace Application.Dtos
{
    public record SetupsDTO
    {
        public record Response(List<Warehouse>? Warehouses = default!, List<BinDetails>? Bins = default!)
        {
            public List<Warehouse>? Warehouses { get; set; } = Warehouses;
            public List<BinDetails>? Bins { get; set; } = Bins;
        }
    }
}
