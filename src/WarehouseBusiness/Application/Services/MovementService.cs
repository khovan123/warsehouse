using Application.Dtos;
using Application.Interfaces;
using Contract.Responses;
using Domain.Enums;
using Domain.Repositories;

namespace Application.Services
{
  public class MovementService : IMovementService
  {
    private readonly IMovementRepository _movementRepository;

    public MovementService(IMovementRepository movementRepository)
    {
      _movementRepository = movementRepository;
    }

    public async Task<ApiResponse<MovementDTO.Response>?> GetAllAsync(CancellationToken ct)
    {
      var movements = await _movementRepository.GetAllAsync(ct);
      var data = new MovementDTO.Response(movements);
      return new ApiResponse<MovementDTO.Response>(data);
    }

    public async Task<ApiResponse<MovementDTO.ReportResponse>?> GetAllWithDetailsAsync(InventoryType? type, SummaryPeriod period, CancellationToken ct)
    {
      var movementReports = await _movementRepository.GetAllWithDetailsAsync(type, period, ct);
      var data = new MovementDTO.ReportResponse(movementReports);
      return new ApiResponse<MovementDTO.ReportResponse>(data);
    }

    public async Task<ApiResponse<MovementDTO.SummaryResponse>?> GetSummaryAsync(SummaryPeriod period, CancellationToken ct)
    {
      var movementSummaries = await _movementRepository.GetSummaryAsync(period, ct);
      var data = new MovementDTO.SummaryResponse(movementSummaries);
      return new ApiResponse<MovementDTO.SummaryResponse>(data);
    }
  }
}