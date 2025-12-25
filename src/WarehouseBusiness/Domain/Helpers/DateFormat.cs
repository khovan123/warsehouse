using Domain.Enums;

namespace Domain.Helpers
{
  public static class DateFormat
  {
    public static string GetDateFormat(SummaryPeriod period)
    {
      return period switch
      {
        SummaryPeriod.Daily => "%Y-%m-%d",
        SummaryPeriod.Weekly => "%Y-W%V",
        SummaryPeriod.Monthly => "%Y-%m",
        _ => "%Y-%m-%d"
      };
    }
  }
}
