using Domain.Entities;

namespace Application.Dtos
{
  public record ReservationDTO
  {
    public record Response(List<Reservation>? Reservations = default!)
    {
      public List<Reservation>? Reservations { get; set; } = Reservations;
    }
  }
}