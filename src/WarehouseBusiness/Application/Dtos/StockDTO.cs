using Domain.Entities.Weak;

namespace Application.Dtos
{
    public record StockDTO
    {
        public record Response(List<StockReport>? Stocks = default!)
        {
            public List<StockReport>? Stocks { get; set; } = Stocks;
        }
    }
}
