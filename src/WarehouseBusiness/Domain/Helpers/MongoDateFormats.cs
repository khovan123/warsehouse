using Domain.Enums;

namespace Domain.Helpers
{
  public static class MongoDateFormats
  {
    public const string Year = "%Y";
    public const string Month = "%m";
    public const string Day = "%d";
    public const string IsoWeek = "%V";
    public static string GetDateFormat(SummaryPeriod period)
    {
      return period switch
      {
        SummaryPeriod.Today => $"{Year}-{Month}-{Day}",
        SummaryPeriod.ThisWeek => $"{Year}-W{IsoWeek}",
        SummaryPeriod.ThisMonth => $"{Year}-{Month}",
        SummaryPeriod.ThisYear => $"{Year}-{Month}-{Day}",
        _ => $"{Year}-{Month}-{Day}"
      };
    }
  }
}
