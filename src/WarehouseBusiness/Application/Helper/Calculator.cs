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
    }
}
