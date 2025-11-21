namespace EximStockTransactions.Infrastructure.Repositories
{
  using Microsoft.EntityFrameworkCore;
  using EximStockTransactions.Application.Interfaces;
  using EximStockTransactions.Domain.Entities;
  using EximStockTransactions.Domain.Models;
  using EximStockTransactions.Infrastructure.Context;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;

  public class ItemRepository : IItemRepository
  {
    private readonly EximStockTransactionsDbContext _context;

    public ItemRepository(EximStockTransactionsDbContext context)
    {
      _context = context;
    }

    public async Task<List<Item>> GetAllAsync()
    {
      return await _context.Items.ToListAsync();
    }

    public async Task<Item?> GetByIdAsync(int id)
    {
      return await _context.Items.FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Item?> GetBySKUAsync(string sku)
    {
      return await _context.Items.FirstOrDefaultAsync(i => i.SKU == sku);
    }

    public async Task AddAsync(Item item)
    {
      _context.Items.Add(item);
      await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Item item)
    {
      _context.Entry(item).State = EntityState.Modified;
      await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Item item)
    {
      _context.Items.Remove(item);
      await _context.SaveChangesAsync();
    }

    public async Task<List<ItemInventorySummary>> GetInventorySummaryAsync(bool onlyLowStock = false)
    {
      var query = _context.Items
          .Include(i => i.Transactions)
          .Select(i => new ItemInventorySummary
          {
            ItemId = i.Id,
            SKU = i.SKU,
            Name = i.Name,
            OnHandQuantity = i.Transactions.Sum(t => t.QuantityChange),
            InventoryValue = i.Transactions.Sum(t => t.QuantityChange) * i.UnitPrice,
            LowStockThreshold = i.LowStockThreshold
          });

      if (onlyLowStock)
      {
        query = query.Where(i => i.OnHandQuantity <= i.LowStockThreshold);
      }

      return await query.ToListAsync();
    }
  }
}
