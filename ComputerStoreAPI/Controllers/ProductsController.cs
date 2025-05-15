using ComputerStoreAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductService _service;

    public ProductsController(ProductService service) { _service = service; }

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var p = await _service.GetAsync(id);
        if (p == null) return NotFound(new { error = "Product not found" });
        return Ok(p);
    }

    [HttpPost]
    public async Task<IActionResult> Post(ProductDto dto)
    {
        try 
        { 
            return Ok(await _service.CreateAsync(dto)); 
        }
        catch (Exception ex) 
        { 
            return BadRequest(new { error = ex.Message }); 
        }
        /*catch (DbUpdateException ex)
        {
            var innerMessage = ex.InnerException?.Message ?? ex.Message;
            throw new Exception($"Database update failed: {innerMessage}");
        } */
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ProductDto dto)
    {
        try
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (!updated) return NotFound(new { error = "Product not found" });
            return Ok();
        }
        catch (Exception ex) { return BadRequest(new { error = ex.Message }); }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound(new { error = "Product not found" });
        return Ok();
    }
}
