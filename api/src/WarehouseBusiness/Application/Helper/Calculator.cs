using Application.Exceptions;
using Domain.Entities;
using Domain.Enums;

namespace Application.Helper
{
    public static class Calculator
    {
        public static double CalculatePercentage(double part, double total, int decimalPlaces = 2)
        {
            if (total <= 0)
                return 0;

            return Math.Round(part / total * 100, decimalPlaces);
        }

        public static double CalculatePercentage(int part, int total, int decimalPlaces = 2)
        {
            if (total <= 0)
                return 0;

            return Math.Round((double)part / total * 100, decimalPlaces);
        }

        public static Stock CalculateStock(Inventory inventory, Stock stock, GoodTransaction goodTransaction)
        {
            Stock stockUpdated = stock;
            switch (inventory.Type)
            {
                case InventoryType.Receipt:
                    stockUpdated.OnHand += inventory.Qty;
                    break;
                case InventoryType.Movement:
                    break;
                case InventoryType.Shipment:
                    stockUpdated.OnHand -= inventory.Qty;
                    break;
                case InventoryType.Inventory:
                    break;
                case InventoryType.Adjustment:
                    break;
                default:
                    throw new InvalidValueType();
            }
            stockUpdated.InventoryValue = stockUpdated.AverageCost * stockUpdated.OnHand;
            return stockUpdated;
        }
    }
}
