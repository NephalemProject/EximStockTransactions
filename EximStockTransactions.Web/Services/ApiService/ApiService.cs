namespace EximStockTransactions.Web.Services
{
  using System;
  using System.Net.Http;
  using System.Net.Http.Json;
  using System.Threading.Tasks;
  using Microsoft.Extensions.Logging;

  public class ApiService : IApiService
  {
    private readonly HttpClient _httpClient;
    private readonly ILogger<ApiService> _logger;
    private readonly NotificationService _notificationService;

    public ApiService(HttpClient httpClient, ILogger<ApiService> logger, NotificationService notificationService)
    {
      _httpClient = httpClient;
      _logger = logger;
      _notificationService = notificationService;
    }

    public async Task<T?> GetAsync<T>(string url)
    {
      try
      {
        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
          return await response.Content.ReadFromJsonAsync<T>();
        }

        await HandleErrorResponse(response, $"GET {url}");
        return default;
      }
      catch (HttpRequestException ex)
      {
        _logger.LogError(ex, "Network error calling {Url}", url);
        _notificationService.ShowError("Network error. Please check your connection.");
        return default;
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Unexpected error calling {Url}", url);
        _notificationService.ShowError("An unexpected error occurred.");
        return default;
      }
    }

    public async Task<T?> PostAsync<T>(string url, object data)
    {
      try
      {
        var response = await _httpClient.PostAsJsonAsync(url, data);

        if (response.IsSuccessStatusCode)
        {
          return await response.Content.ReadFromJsonAsync<T>();
        }

        await HandleErrorResponse(response, $"POST {url}");
        return default;
      }
      catch (HttpRequestException ex)
      {
        _logger.LogError(ex, "Network error calling {Url}", url);
        _notificationService.ShowError("Network error. Please check your connection.");
        return default;
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Unexpected error calling {Url}", url);
        _notificationService.ShowError("An unexpected error occurred.");
        return default;
      }
    }

    // Add the missing non-generic PostAsync method
    public async Task<bool> PostAsync(string url, object data)
    {
      try
      {
        var response = await _httpClient.PostAsJsonAsync(url, data);

        if (response.IsSuccessStatusCode)
        {
          return true;
        }

        await HandleErrorResponse(response, $"POST {url}");
        return false;
      }
      catch (HttpRequestException ex)
      {
        _logger.LogError(ex, "Network error calling {Url}", url);
        _notificationService.ShowError("Network error. Please check your connection.");
        return false;
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Unexpected error calling {Url}", url);
        _notificationService.ShowError("An unexpected error occurred.");
        return false;
      }
    }

    public async Task<bool> PutAsync(string url, object data)
    {
      try
      {
        var response = await _httpClient.PutAsJsonAsync(url, data);

        if (response.IsSuccessStatusCode)
        {
          return true;
        }

        await HandleErrorResponse(response, $"PUT {url}");
        return false;
      }
      catch (HttpRequestException ex)
      {
        _logger.LogError(ex, "Network error calling {Url}", url);
        _notificationService.ShowError("Network error. Please check your connection.");
        return false;
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Unexpected error calling {Url}", url);
        _notificationService.ShowError("An unexpected error occurred.");
        return false;
      }
    }

    public async Task<bool> DeleteAsync(string url)
    {
      try
      {
        var response = await _httpClient.DeleteAsync(url);

        if (response.IsSuccessStatusCode)
        {
          return true;
        }

        await HandleErrorResponse(response, $"DELETE {url}");
        return false;
      }
      catch (HttpRequestException ex)
      {
        _logger.LogError(ex, "Network error calling {Url}", url);
        _notificationService.ShowError("Network error. Please check your connection.");
        return false;
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Unexpected error calling {Url}", url);
        _notificationService.ShowError("An unexpected error occurred.");
        return false;
      }
    }

    private async Task HandleErrorResponse(HttpResponseMessage response, string operation)
    {
      var statusCode = (int)response.StatusCode;
      var errorContent = await response.Content.ReadAsStringAsync();

      _logger.LogWarning("API error {StatusCode} for {Operation}: {Error}", statusCode, operation, errorContent);

      var errorMessage = statusCode switch
      {
        400 => "Bad request. Please check your input.",
        401 => "Authentication required.",
        403 => "You don't have permission to perform this action.",
        404 => "The requested resource was not found.",
        500 => "Server error. Please try again later.",
        _ => "An error occurred while processing your request."
      };

      _notificationService.ShowError(errorMessage);
    }
  }
}