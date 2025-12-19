using Application.Dtos;
using Application.Interfaces;
using Contract.Responses;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
  public class ReservationService : IReservationService
  {
    private readonly IReservationRepository _reservationRepository;

    public ReservationService(IReservationRepository reservationRepository)
    {
      _reservationRepository = reservationRepository;
    }

    public async Task<ApiResponse<ReservationDTO.Response>> GetAll(CancellationToken ct)
    {
      var reservations = await _reservationRepository.GetAll(ct);
      var data = new ReservationDTO.Response(reservations);
      return new ApiResponse<ReservationDTO.Response>(data);
    }
  }
}