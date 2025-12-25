using Application.Dtos;
using Contract.Responses;
using Domain.Enums;

namespace Application.Interfaces
{
  public interface IMovementService
  {
    Task<ApiResponse<MovementDTO.Response>> GetAll(CancellationToken ct);
    Task<ApiResponse<MovementDTO.ReportResponse>> GetReport(CancellationToken ct);
    Task<ApiResponse<MovementDTO.SummaryResponse>> GetSummary(SummaryPeriod period, CancellationToken ct);
  }
}