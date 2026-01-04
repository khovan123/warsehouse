using Domain.Entities.Weak;

namespace Application.Dtos
{
    public sealed record ParetoDTO
    {
        public sealed record Classification(
            string Tag,
            double TagPercentage,
            double ValuePercentage
        );

        public sealed record Response(
            List<ParetoDetails>? Paretos = default!,
            List<Classification>? Classifications = default!
        );
    }
}
