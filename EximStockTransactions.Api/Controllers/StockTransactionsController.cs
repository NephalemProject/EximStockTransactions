using Microsoft.AspNetCore.Mvc;
using EximStockTransactions.Application.Interfaces;
using EximStockTransactions.Application.Services.StockTransactionService;
using EximStockTransactions.Domain.Entities;

namespace EximStockTransactions.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class StockTransactionsController : ControllerBase
  {
    private readonly IStockTransactionService _service;

    public StockTransactionsController(IStockTransactionService service)
    {
      _service = service;
    }

    // Add/record transaction endpoint
    [HttpPost]
    public async Task<IActionResult> RecordTransaction([FromBody] RecordStockTransactionRequest request)
    {
      if (!ModelState.IsValid) return BadRequest(ModelState);

      var transaction = new StockTransaction
      {
        ItemId = request.ItemId,
        QuantityChange = request.QuantityChange,
        Comment = request.Comment,
        Timestamp = DateTime.UtcNow
      };

      var result = await _service.RecordAsync(transaction);
      return Ok(result);
    }

    // Get transaction for item by ID endpoint
    [HttpGet("item/{itemId}")]
    public async Task<IActionResult> GetByItem(int itemId)
    {
      var transactions = await _service.GetByItemAsync(itemId);
      return Ok(transactions);
    }
  }
}
