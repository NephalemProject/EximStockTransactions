namespace EximStockTransactions.Domain.Entities
{
  using System.ComponentModel.DataAnnotations;

  public class Item
  {
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string SKU { get; set; } = null!;

    [Required, MaxLength(100)]
    public string Name { get; set; } = null!;

    [Range(0, double.MaxValue)]
    public decimal UnitPrice { get; set; }

    [Range(0, int.MaxValue)]
    public int OnHandQuantity { get; set; }

    [Range(0, int.MaxValue)]
    public int LowStockThreshold { get; set; }

    public List<StockTransaction> Transactions { get; set; } = new();
  }
}
