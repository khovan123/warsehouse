using Domain.Entities;
using Domain.Entities.Weak;

namespace Application.Dtos
{
    public sealed record SetupsDTO
    {
        public sealed record Response(List<Warehouse>? Warehouses = default!, List<BinDetails>? Bins = default!);
    }
}
