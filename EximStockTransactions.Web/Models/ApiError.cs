namespace EximStockTransactions.Web.Models
{
  public class ApiError
  {
    public bool Success { get; set; }
    public string Error { get; set; } = string.Empty;
    public string TraceId { get; set; } = string.Empty;
  }
}
