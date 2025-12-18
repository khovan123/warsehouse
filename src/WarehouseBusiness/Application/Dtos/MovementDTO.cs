using Domain.Entities;

namespace Application.Dtos
{
  public record MovementDTO
  {
    public record Response(List<Movement>? Movements = default!)
    {
      public List<Movement>? Movements { get; set; } = Movements;
    }
  }
}