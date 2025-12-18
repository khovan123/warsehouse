using Application.Dtos;
using Contract.Responses;

namespace Application.Interfaces
{
  public interface IMovementService
  {
    Task<ApiResponse<MovementDTO.Response>> GetAll(CancellationToken ct);
  }
}