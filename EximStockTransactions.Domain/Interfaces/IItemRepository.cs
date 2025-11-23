namespace EximStockTransactions.Domain.Interfaces
{
  using EximStockTransactions.Domain.Entities;
  using EximStockTransactions.Domain.Models;

  public interface IItemRepository
  {
    Task<List<Item>> GetAllAsync();
    Task<Item?> GetByIdAsync(int id);
    Task<Item?> GetBySKUAsync(string sku);
    Task AddAsync(Item item);
    Task UpdateAsync(Item item);
    Task DeleteAsync(Item item);
    Task<List<ItemInventorySummary>> GetInventorySummaryAsync(bool onlyLowStock = false);
  }
}
