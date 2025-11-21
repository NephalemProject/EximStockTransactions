namespace EximStockTransactions.Application.Services.StockTransactionService
{
  public class StockTransactionResponse
  {
    public int Id { get; set; }
    public int ItemId { get; set; }
    public int QuantityChange { get; set; }
    public DateTime Timestamp { get; set; }
    public string? Comment { get; set; }
  }
}
