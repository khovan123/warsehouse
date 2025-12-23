using Domain.Entities.Weak;

namespace Application.DTOs
{
    public record ParetoDTO
    {
        public record Classification(
            string Tag,
            double TagPercentage,
            double ValuePercentage
        );

        public record Response(
            List<ParetoReport>? Paretos = default!,
            List<Classification>? Classifications = default!
        )
        {
            public List<ParetoReport>? Paretos { get; set; } = Paretos;
            public List<Classification>? Classifications { get; set; } = Classifications;
        }
    }
}
