using Domain.Entities;
using Domain.Entities.Weak.Movement;

namespace Application.Dtos
{
  public record MovementDTO
  {
    public record Response(List<Movement>? Movements = default!)
    {
      public List<Movement>? Movements { get; set; } = Movements;
    }
    
    public record ReportResponse(List<MovementReport>? MovementReports = default!)
    {
      public List<MovementReport>? MovementReports { get; set; } = MovementReports;
    }
    
    public record SummaryResponse(List<MovementSummary>? MovementSummaries = default!)
    {
      public List<MovementSummary>? MovementSummaries { get; set; } = MovementSummaries;
    }
  }
}