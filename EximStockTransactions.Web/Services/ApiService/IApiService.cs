namespace EximStockTransactions.Web.Services
{
  using System.Threading.Tasks;
  public interface IApiService
  {
    Task<T?> GetAsync<T>(string url);
    Task<T?> PostAsync<T>(string url, object data);
    Task<bool> PostAsync(string url, object data);  // This was missing implementation
    Task<bool> PutAsync(string url, object data);
    Task<bool> DeleteAsync(string url);
  }
}