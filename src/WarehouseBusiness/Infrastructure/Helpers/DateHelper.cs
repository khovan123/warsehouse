using Domain.Enums;
using MongoDB.Driver.Linq;

namespace Infrastructure.Helpers
{
  public static class DateHelper
  {
    public enum DATE_RANGE_OPTION
    {
      Today = 0,
      ThisWeek = 1,
      ThisMonth = 2,
      ThisYear = 3,
      Days = 4,
      Week = 5,
      Month = 6,
      Year = 7,
    }
    public static (DateTime startUtc, DateTime endUtc) CreateDateRangeUtc(int startOffsetTimes = 0, int endOffsetTimes = 0, DATE_RANGE_OPTION range_option = DATE_RANGE_OPTION.Days)
    {
      var now = DateTime.UtcNow;

      DateTime start;
      DateTime end;

      int endOffsetTimesConverted = (endOffsetTimes > 0) ? -endOffsetTimes : endOffsetTimes;

      switch (range_option)
      {
        case DATE_RANGE_OPTION.Today:
          start = now.Date.AddDays(startOffsetTimes);
          end = now.Date.AddDays(1);
          break;

        case DATE_RANGE_OPTION.ThisWeek:
          var today = now.Date;
          int diff = (7 + (int)today.DayOfWeek - (int)DayOfWeek.Monday) % 7;
          start = today.AddDays(-diff);
          end = start.AddDays(7);
          break;

        case DATE_RANGE_OPTION.ThisMonth:
          start = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);
          end = start.AddMonths(1);
          break;

        case DATE_RANGE_OPTION.ThisYear:
          start = new DateTime(now.Year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
          end = start.AddYears(1);
          break;

        case DATE_RANGE_OPTION.Days:
          start = now.Date.AddDays(endOffsetTimesConverted);
          end = now.Date.AddDays(startOffsetTimes);
          break;

        case DATE_RANGE_OPTION.Week:
          start = now.Date.AddWeeks(endOffsetTimesConverted);
          end = now.Date.AddWeeks(startOffsetTimes);
          break;

        case DATE_RANGE_OPTION.Month:
          start = now.Date.AddMonths(endOffsetTimesConverted);
          end = now.Date.AddMonths(startOffsetTimes);
          break;

        case DATE_RANGE_OPTION.Year:
          start = now.Date.AddYears(endOffsetTimesConverted);
          end = now.Date.AddYears(startOffsetTimes);
          break;

        default:
          start = now.Date;
          end = start.AddDays(1);
          break;
      }
      return (start, end);
    }

    public static (DateTime startUtc, DateTime endUtc) CreateDateRangeUtc(int startOffsetTimes = 0, int endOffsetTimes = 0, SummaryPeriod period = SummaryPeriod.Today)
    {
      var option = period switch
      {
        SummaryPeriod.Today => DATE_RANGE_OPTION.Today,
        SummaryPeriod.ThisWeek => DATE_RANGE_OPTION.ThisWeek,
        SummaryPeriod.ThisMonth => DATE_RANGE_OPTION.ThisMonth,
        SummaryPeriod.ThisYear => DATE_RANGE_OPTION.ThisYear,
        _ => DATE_RANGE_OPTION.Today
      };

      return CreateDateRangeUtc(startOffsetTimes, endOffsetTimes, option);
    }
  }
}