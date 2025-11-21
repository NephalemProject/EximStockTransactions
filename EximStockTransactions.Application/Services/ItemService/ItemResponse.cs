namespace EximStockTransactions.Application.Services.ItemService
{
  public class ItemResponse
  {
    public int Id { get; set; }
    public string SKU { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal UnitPrice { get; set; }
    public int OnHandQuantity { get; set; }
    public int LowStockThreshold { get; set; }
  }
}
