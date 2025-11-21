using System.ComponentModel.DataAnnotations;

namespace EximStockTransactions.Application.Services.StockTransactionService
{
  public class RecordStockTransactionRequest
  {
    [Required]
    public int ItemId { get; set; }

    [Required]
    public int QuantityChange { get; set; }

    [MaxLength(200)]
    public string? Comment { get; set; }
  }
}
