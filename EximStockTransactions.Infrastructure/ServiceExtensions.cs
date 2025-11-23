namespace EximStockTransactions.Infrastructure
{
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;
  using EximStockTransactions.Domain.Interfaces;
  using EximStockTransactions.Infrastructure.Context;
  using EximStockTransactions.Infrastructure.Repositories;

  public static class ServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Database
            services.AddDbContext<EximStockTransactionsDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("SQLite")));

            // Repositories
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IStockTransactionRepository, StockTransactionRepository>();


            return services;
        }
    }
}
