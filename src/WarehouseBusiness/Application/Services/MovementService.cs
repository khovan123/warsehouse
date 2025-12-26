using Application.Dtos;
using Application.Interfaces;
using Contract.Responses;
using Domain.Entities.Weak;
using Domain.Enums;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
  public class MovementService : IMovementService
  {
    private readonly IMovementRepository _movementRepository;

    public MovementService(IMovementRepository movementRepository)
    {
      _movementRepository = movementRepository;
    }
    
    public async Task<ApiResponse<MovementDTO.Response>> GetAll(CancellationToken ct)
    {
      var movements = await _movementRepository.GetAll(ct);
      var data = new MovementDTO.Response(movements);
      return new ApiResponse<MovementDTO.Response>(data);
    }
    
    public async Task<ApiResponse<MovementDTO.ReportResponse>> GetAsReport(CancellationToken ct)
    {
      var movementReports = await _movementRepository.GetAllWithDetails(ct);
      var data = new MovementDTO.ReportResponse(movementReports);
      return new ApiResponse<MovementDTO.ReportResponse>(data);
    }
    
    public async Task<ApiResponse<MovementDTO.SummaryResponse>> GetAsSummary(SummaryPeriod period, CancellationToken ct)
    {
      var movementSummaries = await _movementRepository.GetSummary(period, ct);
      var data = new MovementDTO.SummaryResponse(movementSummaries);
      return new ApiResponse<MovementDTO.SummaryResponse>(data);
    }
  }
}