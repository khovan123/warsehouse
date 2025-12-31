using Domain.Entities.Weak;

namespace Application.Dtos
{
  public sealed record ReservationDTO
  {
    public sealed record Response(List<ReservationDetails>? Reservations = default!);
  }
}