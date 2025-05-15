using ComputerStoreAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class StockImportController : ControllerBase
{
    private readonly StockImportService _service;
    public StockImportController(StockImportService service) { _service = service; }

    [HttpPost]
    public async Task<IActionResult> Import([FromBody] List<StockImportDto> importList)
    {
        try
        {
            await _service.ImportAsync(importList);
            return Ok();
        }
        catch (Exception ex) { return BadRequest(new { error = ex.Message }); }
    }
}
