namespace EximStockTransactions.Application.Helpers
{
  using EximStockTransactions.Application.Services.ItemService;
  using EximStockTransactions.Domain.Entities;

  public static class ItemMapper
  {
    public static ItemResponse MapToResponse(Item item) => new ItemResponse
    {
      Id = item.Id,
      SKU = item.SKU,
      Name = item.Name,
      UnitPrice = item.UnitPrice,
      OnHandQuantity = item.OnHandQuantity,
      LowStockThreshold = item.LowStockThreshold
    };

    public static InventorySummaryResponse MapToSummaryResponse(Item item) => new InventorySummaryResponse
    {
      Id = item.Id,
      SKU = item.SKU,
      Name = item.Name,
      OnHandQuantity = item.OnHandQuantity,
      UnitPrice = item.UnitPrice,
      InventoryValue = item.UnitPrice * item.OnHandQuantity,
      LowStockThreshold = item.LowStockThreshold
    };
  }
}

