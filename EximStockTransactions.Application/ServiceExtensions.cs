namespace EximStockTransactions.Application
{
  using Microsoft.Extensions.DependencyInjection;
  using EximStockTransactions.Application.Interfaces;
  using EximStockTransactions.Application.Services.ItemService;

  public static class ServiceExtensions
  {
      public static IServiceCollection AddApplicationServices(this IServiceCollection services)
      {
        services.AddScoped<IItemService, ItemService>();
        services.AddScoped<IStockTransactionService, StockTransactionService>();
  
        return services;
      }
  }
}
