using System.Text.Json.Serialization;

namespace Domain.Enums
{
  [JsonConverter(typeof(JsonStringEnumConverter))]
  public enum SummaryPeriod
  {
    Daily = 0,
    Weekly = 1,
    Monthly = 2,
    Today,
    ThisWeek,
    ThisMonth,
    ThisYear,

  }
}
