namespace EximStockTransactions.Tests 
{
  using EximStockTransactions.Application.Interfaces;
  using EximStockTransactions.Application.Services.ItemService;
  using EximStockTransactions.Domain.Entities;
  using Microsoft.Extensions.Logging;
  using Moq;
  using Xunit;

  public class ItemBusinessRuleTests
  {
    private readonly Mock<IItemRepository> _mockRepo;
    private readonly ItemService _itemService;

    public ItemBusinessRuleTests()
    {
      _mockRepo = new Mock<IItemRepository>();

      // Mock the logger
      var mockLogger = new Mock<ILogger<ItemService>>();
      _itemService = new ItemService(_mockRepo.Object, mockLogger.Object);
    }
    [Fact]
    public async Task GetLowStockCount_WhenItemIsBelowThreshold_CountsAsLowStock()
    {
      // Arrange
      var items = new List<Item>
        {
            new() { OnHandQuantity = 2, LowStockThreshold = 5 }
        };

      _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(items);

      // Act
      var result = await _itemService.GetLowStockCountAsync();

      // Assert
      Assert.Equal(1, result); // Should count as low stock
    }

    [Fact]
    public async Task GetLowStockCount_WhenItemIsAtThreshold_CountsAsLowStock()
    {
      // Arrange
      var items = new List<Item>
        {
            new() { OnHandQuantity = 5, LowStockThreshold = 5 }
        };

      _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(items);

      // Act
      var result = await _itemService.GetLowStockCountAsync();

      // Assert
      Assert.Equal(1, result); // Should count as low stock (at threshold)
    }

    [Fact]
    public async Task GetLowStockCount_WhenItemIsAboveThreshold_DoesNotCountAsLowStock()
    {
      // Arrange
      var items = new List<Item>
        {
            new() { OnHandQuantity = 10, LowStockThreshold = 5 } // 10 > 5 = NOT LOW STOCK
        };

      _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(items);

      // Act
      var result = await _itemService.GetLowStockCountAsync();

      // Assert
      Assert.Equal(0, result); // Should NOT count as low stock
    }

    [Fact]
    public async Task GetInventorySummary_WithLowStockFilter_ExcludesNormalStockItems()
    {
      // Arrange
      var items = new List<Item>
        {
            new() { Id = 1, Name = "Low Stock Item", OnHandQuantity = 3, LowStockThreshold = 5 },
            new() { Id = 2, Name = "Normal Stock Item", OnHandQuantity = 8, LowStockThreshold = 5 }
        };

      _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(items);

      // Act
      var result = await _itemService.GetInventorySummaryAsync(lowStockOnly: true);

      // Assert
      Assert.Single(result.Items); // Only one item (the low stock one)
      Assert.Equal("Low Stock Item", result.Items[0].Name);
      Assert.Equal(3, result.Items[0].OnHandQuantity);
    }

    [Fact]
    public async Task InventoryValue_IsCalculatedCorrectly()
    {
      // Arrange
      var items = new List<Item>
        {
            new() { OnHandQuantity = 5, UnitPrice = 10.0m },  // 5 * 10 = 50
            new() { OnHandQuantity = 2, UnitPrice = 15.0m }   // 2 * 15 = 30
        };

      _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(items);

      // Act
      var result = await _itemService.GetInventorySummaryAsync(lowStockOnly: false);

      // Assert
      Assert.Equal(80.0m, result.TotalInventoryValue); // 50 + 30 = 80
    }

    [Fact]
    public async Task ZeroQuantityItems_AreHandledCorrectly()
    {
      // Arrange
      var items = new List<Item>
        {
            new() { OnHandQuantity = 0, LowStockThreshold = 5 } // 0 < 5 = LOW STOCK
        };

      _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(items);

      // Act
      var result = await _itemService.GetLowStockCountAsync();

      // Assert
      Assert.Equal(1, result); // Zero quantity should count as low stock
    }
  }

};