using System.Text.Json.Serialization;

namespace Domain.Enums
{
  [JsonConverter(typeof(JsonStringEnumConverter))]
  public enum SummaryPeriod
  {
    Today = 0,
    ThisWeek = 1,
    ThisMonth = 2,
    ThisYear = 3
  }
}
