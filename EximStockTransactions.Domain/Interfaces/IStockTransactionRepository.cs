namespace EximStockTransactions.Domain.Interfaces
{
  using EximStockTransactions.Domain.Entities;

  public interface IStockTransactionRepository
  {
    Task AddAsync(StockTransaction transaction);
    Task<List<StockTransaction>> GetTransactionsByItemIdAsync(int itemId);
  }
}
