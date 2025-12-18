using Domain.Entities;

namespace Application.Dtos
{
    public record BinDTO
    {
        public record Response(List<Bin>? Bins = default!)
        {
            public List<Bin>? Bins { get; set; } = Bins;
        }
    }
}
