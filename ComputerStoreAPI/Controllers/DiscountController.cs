using ComputerStoreAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class DiscountController : ControllerBase
{
    private readonly DiscountService _service;
    public DiscountController(DiscountService service) { _service = service; }

    [HttpPost]
    public async Task<IActionResult> Calculate([FromBody] List<BasketItemDto> basket)
    {
        var result = await _service.CalculateDiscountAsync(basket);
        if (!string.IsNullOrEmpty(result.Error))
            return BadRequest(new { error = result.Error });
        return Ok(result);
    }
}
