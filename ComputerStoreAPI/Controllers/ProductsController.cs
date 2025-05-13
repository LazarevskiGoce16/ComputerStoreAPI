using ComputerStoreAPI.DTOs;
using ComputerStoreAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductResponseDto>>> GetAllProducts()
        {
            return Ok(await _productService.GetAllProductsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseDto>> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound("Product not found!");
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<List<ProductResponseDto>>> CreateProduct([FromBody] ProductDto productDto)
        {
            return Ok(await _productService.CreateProductAsync(productDto));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<ProductResponseDto>>> UpdateProduct(int id, [FromBody] ProductDto productDto)
        {
            var result = await _productService.UpdateProductAsync(id, productDto);
            if (result == null)
                return NotFound("Product not found!");
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<ProductResponseDto>>> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if (result == null)
                return NotFound("Product not found!");
            return Ok(result);
        }
    }
}
