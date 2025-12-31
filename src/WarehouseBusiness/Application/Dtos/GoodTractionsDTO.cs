using Domain.Entities.Weak;

namespace Application.Dtos
{
  public sealed record GoodTransactionDTO
  {
    public sealed record Response(List<GoodTransactionDetails>? GoodTransactions = default!);
  }
}