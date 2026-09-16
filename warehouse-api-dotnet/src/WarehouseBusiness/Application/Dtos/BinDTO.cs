using Domain.Entities.Weak;

namespace Application.Dtos
{
    public sealed record BinDTO
    {
        public sealed record Response(List<BinDetails>? Bins = default!);
    }
}
