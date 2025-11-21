namespace EximStockTransactions.Application.Interfaces
{
  using EximStockTransactions.Application.Services.StockTransactionService;
  using EximStockTransactions.Domain.Entities;

  public interface IStockTransactionService
  {
    Task<StockTransactionResponse> RecordAsync(StockTransaction transaction);
    Task<IEnumerable<StockTransactionResponse>> GetByItemAsync(int itemId);
  }
}
