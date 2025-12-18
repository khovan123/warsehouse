using Application.Dtos;
using Application.Interfaces;
using Contract.Responses;
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
      return new ApiResponse<MovementDTO.Response>.SuccessBuilder(data, "Get resources successfully!", StatusCodes.Status200OK);
    }
  }
}