using Microsoft.AspNetCore.Mvc;
using EximStockTransactions.Application.Interfaces;
using EximStockTransactions.Domain.Entities;
using EximStockTransactions.Application.Services.ItemService;

namespace EximStockTransactions.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ItemsController : ControllerBase
  {
    private readonly IItemService _itemService;

    public ItemsController(IItemService itemService)
    {
      _itemService = itemService;
    }

    // Get All endpoint
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var items = await _itemService.GetAllAsync();
      return Ok(items);
    }

    // Get by ID endpoint
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
      var item = await _itemService.GetByIdAsync(id);
      if (item == null)
      {
        return NotFound();
      }

      return Ok(item);
    }

    // Add item endpoint
    [HttpPost]
    public async Task<IActionResult> Add(Item item)
    {
      var response = await _itemService.AddAsync(item);
      return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    // Update item endpoint
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Item item)
    {
      if (id != item.Id)
      {
        return BadRequest("ID mismatch");
      }

      try
      {
        await _itemService.UpdateAsync(item);
        return NoContent();
      }
      catch (Exception ex)
      {
        return NotFound($"Item with ID {id} not found");
      }
    }

    // Delete item endpoint
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      try
      {
        await _itemService.DeleteAsync(id);
        return NoContent();
      }
      catch (Exception ex)
      {
        return NotFound($"Item with ID {id} not found");
      }
    }

    // Get inventory summary
    [HttpGet("summary")]
    public async Task<IActionResult> Summary(bool lowStockOnly = false)
    {
      var summary = await _itemService.GetInventorySummaryAsync(lowStockOnly);
      return Ok(summary);
    }

    // Aggregate query for low stock
    [HttpGet("low-stock-count")]
    public async Task<ActionResult<int>> GetLowStockCount()
    {
      var count = await _itemService.GetLowStockCountAsync();
      return Ok(count);
    }
  }
}
