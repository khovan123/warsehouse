using Domain.Entities;
using Domain.Entities.Weak;

namespace Application.Dtos
{
    public sealed record InventoryDTO
    {
        public record ResponseDetails(List<InventoryDetails>? Inventories = default!);
    }
}
