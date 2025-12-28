using Domain.Entities;
using Domain.Entities.Weak;

namespace Application.Dtos
{
    public record BinDTO
    {
        public record Response(List<BinDetails>? Bins = default!)
        {
            public List<BinDetails>? Bins { get; set; } = Bins;
        }
    }
}
