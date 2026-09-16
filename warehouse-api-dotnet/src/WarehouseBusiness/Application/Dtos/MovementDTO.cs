using Domain.Entities.Weak;
using Domain.Entities.Weak.Movement;
using Domain.Enums;

namespace Application.Dtos
{
  public sealed record MovementDTO
  {
    public sealed record Response(List<MovementDetails>? Movements = default!);

    public sealed record ReportResponse(List<MovementReport>? MovementReports = default!);

    public sealed record SummaryResponse(List<MovementSummary>? MovementSummaries = default!);

    public record MovementLine(
      string ProductId,
      int Qty,
      string Uom,
      string FromBin,
      string ToBin
    );

    public record CreateRequest(
      string DocNo,
      DateOnly PostedAt,
      string FromWarehouse,
      string ToWarehouse,
      string Reason,
      string CreatedBy,
      List<MovementLine> Lines
    );

    public record CreateResponse(string Id);

    public record ChangeStatusResponse(string Id, FlowStatus Status);

    public record PostResponse(string Id, string Message);
  }
}