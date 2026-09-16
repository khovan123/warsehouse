using Domain.Entities;

namespace Application.Dtos
{
  public record GoodTransactionDTO
  {
    public record Response(List<GoodTransaction>? GoodTransactions = default!)
    {
      public List<GoodTransaction>? GoodTransactions { get; set; } = GoodTransactions;
    }
  }
}