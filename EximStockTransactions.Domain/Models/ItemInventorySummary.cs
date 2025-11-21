namespace EximStockTransactions.Domain.Models
{
  public class ItemInventorySummary
  {
    public int ItemId { get; set; }
    public string SKU { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int OnHandQuantity { get; set; }
    public decimal InventoryValue { get; set; }
    public int LowStockThreshold { get; set; }
  }
}
