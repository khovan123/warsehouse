using Domain.Entities.Weak;

namespace Application.DTOs
{
    public record StockDTO
    {
        public record Response(List<StockReport>? Stocks = default!)
        {
            public List<StockReport>? Stocks { get; set; } = Stocks;
        }
    }
}
