namespace EximStockTransactions.Web.Services
{
  using System;
  using System.Threading.Tasks;

  public class NotificationService
  {
    public event Func<string, string, Task>? OnShow;

    public void ShowError(string message, string title = "Error")
    {
      OnShow?.Invoke(message, title);
    }

    public void ShowSuccess(string message, string title = "Success")
    {
      OnShow?.Invoke(message, title);
    }

    public void ShowWarning(string message, string title = "Warning")
    {
      OnShow?.Invoke(message, title);
    }
  }
}