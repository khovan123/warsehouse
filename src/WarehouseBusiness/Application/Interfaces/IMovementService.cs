using Application.Dtos;
using Contract.Responses;
using Domain.Enums;

namespace Application.Interfaces
{
  public interface IMovementService
  {

    Task<ApiResponse<MovementDTO.Response>?> GetAllAsync(CancellationToken ct);
    Task<ApiResponse<MovementDTO.ReportResponse>?> GetAllWithDetailsAsync(InventoryType? type, SummaryPeriod period, CancellationToken ct);
    Task<ApiResponse<MovementDTO.SummaryResponse>?> GetSummaryAsync(SummaryPeriod period, CancellationToken ct);
    Task<ApiResponse<MovementDTO.MaterialTransactionResponse>> GetMaterialTransactionAsync(InventoryType? type, string? warehouseId, CancellationToken ct);
  }
}