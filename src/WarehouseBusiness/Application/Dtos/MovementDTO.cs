using Domain.Entities.Weak;
using Domain.Entities.Weak.Movement;

namespace Application.Dtos
{
  public sealed record MovementDTO
  {
    public sealed record Response(List<MovementDetails>? Movements = default!);

    public sealed record ReportResponse(List<MovementReport>? MovementReports = default!);

    public sealed record SummaryResponse(List<MovementSummary>? MovementSummaries = default!);
    public record MaterialTransactionResponse(List<MaterialTransaction>? MaterialTransactions = default!)
    {
      public List<MaterialTransaction>? MaterialTransactions { get; set; } = MaterialTransactions;
    }
  }
}