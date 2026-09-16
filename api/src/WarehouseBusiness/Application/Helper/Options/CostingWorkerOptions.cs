namespace Application.Helper.Options
{
  public class CostingWorkerOptions
  {
    public int BatchSize { get; set; } = 200;
    public int PollDelayMs { get; set; } = 500;     // delay khi không có dữ liệu
    public int ErrorDelayMs { get; set; } = 2000;   // delay khi lỗi
  }
}
