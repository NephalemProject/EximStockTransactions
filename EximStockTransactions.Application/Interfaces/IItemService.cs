namespace EximStockTransactions.Application.Interfaces
{
  using EximStockTransactions.Application.Services.ItemService;
  using EximStockTransactions.Domain.Entities;

  public interface IItemService
  {
    Task<IEnumerable<ItemResponse>> GetAllAsync();
    Task<ItemResponse?> GetByIdAsync(int id);
    Task<ItemResponse> AddAsync(Item item);
    Task UpdateAsync(Item item);
    Task DeleteAsync(int id);
    Task<InventorySummaryResult> GetInventorySummaryAsync(bool lowStockOnly);
    Task<int> GetLowStockCountAsync();

  }
}
