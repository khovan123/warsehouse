using Domain.Entities.Weak;
using Domain.Enums;

namespace Application.Dtos
{
  public sealed record GoodTransactionDTO
  {
    public sealed record Response(List<GoodTransactionDetails>? GoodTransactions = default!);

    public record Line(
      string ProductId,
      int ExpectedQty,
      int CountedQty,
      int Difference,
      string BinId,
      string Uom
    );

    public record CreateRequest(
      string DocNo,
      string WarehouseId,
      DateOnly PostedAt,
      string Description,
      string CreatedBy,
      List<Line> Lines
    );

    public record CreateResponse(string Id);

    public record ChangeStatusResponse(string Id, FlowStatus Status);

    public record PostResponse(string Id, string Message);
  }
}