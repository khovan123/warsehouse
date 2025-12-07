using Domain.Entities;

namespace Application.DTOs
{
    public record BinDTO
    {
        public record Response(List<Bin>? Bins = default!)
        {
            public List<Bin>? Bins = Bins;
        }
    }
}
