using ComputerStoreAPI.DTOs;
using ComputerStoreAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryResponseDto>>> GetAllCategories()
        {
            return Ok(await _categoryService.GetAllCategoriesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResponseDto>> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound("Category not found!");
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<List<CategoryResponseDto>>> CreateCategory([FromBody] CategoryDto categoryDto)
        {
            return Ok(await _categoryService.CreateCategoryAsync(categoryDto));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<CategoryResponseDto>>> UpdateCategory(int id, [FromBody] CategoryDto categoryDto)
        {
            var result = await _categoryService.UpdateCategoryAsync(id, categoryDto);
            if (result == null)
                return NotFound("Category not found!");
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<CategoryResponseDto>>> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            if (result == null)
                return NotFound("Category not found!");
            return Ok(result);
        }
    }
}
