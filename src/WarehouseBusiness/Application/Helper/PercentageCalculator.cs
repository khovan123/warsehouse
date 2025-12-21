namespace Application.Helper
{
    public static class PercentageCalculator
    {
        public static double Calculate(double part, double total, int decimalPlaces = 2)
        {
            if (total <= 0)
                return 0;

            return Math.Round(part / total * 100, decimalPlaces);
        }

        public static double Calculate(int part, int total, int decimalPlaces = 2)
        {
            if (total <= 0)
                return 0;

            return Math.Round((double)part / total * 100, decimalPlaces);
        }
    }
}
