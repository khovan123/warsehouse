using Domain.Entities;
using Domain.Entities.Weak;

namespace Application.Dtos
{
  public record ReservationDTO
  {
    public record Response(List<ReservationDetails>? Reservations = default!)
    {
      public List<ReservationDetails>? Reservations { get; set; } = Reservations;
    }
  }
}