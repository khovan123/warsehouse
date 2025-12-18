using Application.Dtos;
using Contract.Responses;

namespace Application.Interfaces
{
  public interface IReservationService
  {
    Task<ApiResponse<ReservationDTO.Response>> GetAll(CancellationToken ct);

  }
}