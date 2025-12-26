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
        SummaryPeriod.Daily => $"{Year}-{Month}-{Day}",
        SummaryPeriod.Weekly => $"{Year}-W{IsoWeek}",
        SummaryPeriod.Monthly => $"{Year}-{Month}",
        _ => $"{Year}-{Month}-{Day}"
      };
    }
  }
}
