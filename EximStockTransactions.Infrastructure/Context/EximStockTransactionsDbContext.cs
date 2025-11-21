namespace EximStockTransactions.Infrastructure.Context
{
  using Microsoft.EntityFrameworkCore;
  using EximStockTransactions.Domain.Entities;

  public class EximStockTransactionsDbContext : DbContext
  {
    public EximStockTransactionsDbContext(DbContextOptions<EximStockTransactionsDbContext> options)
        : base(options) { }

    public DbSet<Item> Items => Set<Item>();
    public DbSet<StockTransaction> StockTransactions => Set<StockTransaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // Seed example data
      modelBuilder.Entity<Item>().HasData(
          new Item { Id = 1, SKU = "ABC123", Name = "Item A", UnitPrice = 10.50m, LowStockThreshold = 5 },
          new Item { Id = 2, SKU = "DEF456", Name = "Item B", UnitPrice = 20.00m, LowStockThreshold = 3 }
      );

      modelBuilder.Entity<StockTransaction>().HasData(
          new StockTransaction { Id = 1, ItemId = 1, QuantityChange = 50, Timestamp = new DateTime(2025, 11, 20, 12, 0, 0), Comment = "Initial Id 1 stock" },
          new StockTransaction { Id = 2, ItemId = 2, QuantityChange = 30, Timestamp = new DateTime(2025, 11, 20, 12, 0, 0), Comment = "Initial Id 2 Stock" }
      );
    }
  }
}
