using EximStockTransactions.Domain.Entities;
using EximStockTransactions.Domain.Models;

namespace EximStockTransactions.Application.Interfaces
{
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
