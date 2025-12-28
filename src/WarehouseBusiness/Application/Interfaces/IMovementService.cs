using Application.Dtos;
using Contract.Responses;
using Domain.Enums;

namespace Application.Interfaces
{
  public interface IMovementService
  {
    Task<ApiResponse<MovementDTO.Response>> GetAll(CancellationToken ct);
    Task<ApiResponse<MovementDTO.ReportResponse>> GetAsReport(InventoryType? type, SummaryPeriod period, CancellationToken ct);
    Task<ApiResponse<MovementDTO.SummaryResponse>> GetAsSummary(SummaryPeriod period, CancellationToken ct);
  }
}