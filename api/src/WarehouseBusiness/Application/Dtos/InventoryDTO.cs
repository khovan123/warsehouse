using Domain.Entities.Weak;
using Domain.Enums;

namespace Application.Dtos
{
    public sealed record InventoryDTO
    {
        public record ResponseDetails(List<InventoryDetails>? Inventories = default!);

        public record CreateRequest(
            string Document,
            int Line,
            InventoryType Type,
            DateOnly PostedAt,
            string ProductId,
            string WarehouseId,
            string BinId,
            int Qty,
            string Uom,
            decimal UnitCost,
            string BpartnerId
        );

        public record CreateResponse(string Id);

        public record PostResponse(string Id, string Message);
    }
}
