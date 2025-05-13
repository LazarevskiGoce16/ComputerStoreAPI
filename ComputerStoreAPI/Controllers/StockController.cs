using ComputerStoreAPI.DTOs;
using ComputerStoreAPI.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class StockController : ControllerBase
{
    private readonly IStockImportService _stockService;

    public StockController(IStockImportService stockService)
    {
        _stockService = stockService;
    }

    [HttpPost("import")]
    public async Task<IActionResult> ImportStock([FromBody] List<ProductStockDto> stockDtos)
    {
        if (stockDtos == null || !stockDtos.Any())
            return BadRequest("No stock data provided.");

        await _stockService.ImportStockAsync(stockDtos);
        return Ok("Stock imported successfully.");
    }
}
