namespace EximStockTransactions.Application.Services.ItemService
{
  public class InventorySummaryResponse
  {
    public int Id { get; set; }
    public string SKU { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int OnHandQuantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal InventoryValue { get; set; }
    public int LowStockThreshold { get; set; }
  }

  public class InventorySummaryResult
  {
    public List<InventorySummaryResponse> Items { get; set; } = new();
    public decimal TotalInventoryValue { get; set; }
  }
}
