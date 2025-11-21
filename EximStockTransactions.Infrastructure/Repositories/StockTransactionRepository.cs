namespace EximStockTransactions.Infrastructure.Repositories
{
  using Microsoft.EntityFrameworkCore;
  using EximStockTransactions.Application.Interfaces;
  using EximStockTransactions.Domain.Entities;
  using EximStockTransactions.Infrastructure.Context;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;

  public class StockTransactionRepository : IStockTransactionRepository
  {
    private readonly EximStockTransactionsDbContext _context;

    public StockTransactionRepository(EximStockTransactionsDbContext context)
    {
      _context = context;
    }

    public async Task<List<StockTransaction>> GetTransactionsByItemIdAsync(int itemId)
    {
      var query = _context.StockTransactions
          .Where(t => t.ItemId == itemId)
          .OrderByDescending(t => t.Timestamp);

      return await query.ToListAsync();
    }

    public async Task AddAsync(StockTransaction transaction)
    {
      _context.StockTransactions.Add(transaction);
      await _context.SaveChangesAsync();
    }
  }
}
