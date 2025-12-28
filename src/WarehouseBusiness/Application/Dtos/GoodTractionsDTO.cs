using Domain.Entities;
using Domain.Entities.Weak;

namespace Application.Dtos
{
  public record GoodTransactionDTO
  {
    public record Response(List<GoodTransactionDetails>? GoodTransactions = default!)
    {
      public List<GoodTransactionDetails>? GoodTransactions { get; set; } = GoodTransactions;
    }
  }
}