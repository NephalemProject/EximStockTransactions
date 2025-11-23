namespace EximStockTransactions.Application.Services.ItemService
{
  using Microsoft.Extensions.Logging;
  using EximStockTransactions.Application.Interfaces;
  using EximStockTransactions.Application.Helpers;
  using EximStockTransactions.Domain.Interfaces;
  using EximStockTransactions.Domain.Entities;

  public class ItemService : IItemService
  {
    private readonly IItemRepository _repository;
    private readonly ILogger<ItemService> _logger;

    public ItemService(IItemRepository repository, ILogger<ItemService> logger)
    {
      _repository = repository;
      _logger = logger;
    }

    public async Task<IEnumerable<ItemResponse>> GetAllAsync()
    {
      var items = await _repository.GetAllAsync();
      return items.Select(ItemMapper.MapToResponse);
    }

    public async Task<ItemResponse?> GetByIdAsync(int id)
    {
      var item = await _repository.GetByIdAsync(id);
      return item == null ? null : ItemMapper.MapToResponse(item);
    }

    public async Task<ItemResponse> AddAsync(Item item)
    {
      try
      {
        var alreadyExists = await _repository.GetBySKUAsync(item.SKU);

        if (alreadyExists != null)
        {
          throw new InvalidOperationException($"An item with SKU: {item.SKU} already exists.");
        }

        await _repository.AddAsync(item);
        return ItemMapper.MapToResponse(item);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Failed to add new item.");
        throw;
      }
    }


    public async Task UpdateAsync(Item item)
    {
      try
      {
        await _repository.UpdateAsync(item);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Failed to update item {ItemId}", item.Id);
        throw;
      }
    }

    public async Task DeleteAsync(int id)
    {
      try
      {
        var item = await _repository.GetByIdAsync(id);
        if (item != null)
        {
          await _repository.DeleteAsync(item);
        }
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Failed to delete item {ItemId}", id);
        throw;
      }
    }

    public async Task<int> GetLowStockCountAsync()
    {
      var items = await _repository.GetAllAsync();
      return items.Count(i => i.OnHandQuantity <= i.LowStockThreshold);
    }

    public async Task<InventorySummaryResult> GetInventorySummaryAsync(bool lowStockOnly)
    {
      var items = await _repository.GetAllAsync();

      if (lowStockOnly)
      {
        items = items.Where(i => i.OnHandQuantity < i.LowStockThreshold).ToList();
      }

      var summaryItems = items.Select(ItemMapper.MapToSummaryResponse).ToList();

      return new InventorySummaryResult
      {
        Items = summaryItems,
        TotalInventoryValue = summaryItems.Sum(x => x.InventoryValue)
      };
    }
  }
}