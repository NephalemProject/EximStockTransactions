using Microsoft.Extensions.Logging;
using EximStockTransactions.Application.Interfaces;
using EximStockTransactions.Application.Services.StockTransactionService;
using EximStockTransactions.Domain.Entities;

public class StockTransactionService : IStockTransactionService
{
  private readonly IStockTransactionRepository _transactionRepository;
  private readonly IItemRepository _itemRepository;
  private readonly ILogger<StockTransactionService> _logger;

  public StockTransactionService(
      IStockTransactionRepository transactionRepository,
      IItemRepository itemRepository,
      ILogger<StockTransactionService> logger)
  {
    _transactionRepository = transactionRepository;
    _itemRepository = itemRepository;
    _logger = logger;
  }

  public async Task<StockTransactionResponse> RecordAsync(StockTransaction transaction)
  {
    // NO TRY-CATCH - let the controller handle the exception
    transaction.Timestamp = DateTime.Now;

    // Update item's OnHandQuantity
    var item = await _itemRepository.GetByIdAsync(transaction.ItemId);
    if (item == null)
    {
      _logger.LogError("Failed to record transaction: Item {ItemId} not found", transaction.ItemId);
      throw new Exception($"Item {transaction.ItemId} not found.");
    }

    item.OnHandQuantity += transaction.QuantityChange;
    if (item.OnHandQuantity < 0)
    {
      _logger.LogWarning("Item {ItemId} stock would go negative. Clamping to zero.", transaction.ItemId);
      item.OnHandQuantity = 0;
    }

    await _itemRepository.UpdateAsync(item);
    await _transactionRepository.AddAsync(transaction);

    _logger.LogInformation("Recorded transaction for item {ItemId}. Quantity change: {QuantityChange}",
        transaction.ItemId, transaction.QuantityChange);

    return MapToResponse(transaction);
  }

  public async Task<IEnumerable<StockTransactionResponse>> GetByItemAsync(int itemId)
  {
    // NO TRY-CATCH - simple query, let it bubble up
    var transactions = await _transactionRepository.GetTransactionsByItemIdAsync(itemId);
    return transactions.Select(MapToResponse);
  }

  private static StockTransactionResponse MapToResponse(StockTransaction transaction)
  {
    return new StockTransactionResponse
    {
      Id = transaction.Id,
      ItemId = transaction.ItemId,
      QuantityChange = transaction.QuantityChange,
      Timestamp = transaction.Timestamp,
      Comment = transaction.Comment
    };
  }
}