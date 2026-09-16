using Domain.Entities.Weak;

namespace Application.Dtos
{
    public sealed record StockDTO
    {
        public sealed record Response(List<StockDetails>? Stocks = default!);
    }
}
