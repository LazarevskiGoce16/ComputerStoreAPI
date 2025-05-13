using ComputerStoreAPI.DTOs;

namespace ComputerStoreAPI.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryResponseDto>> GetAllCategoriesAsync();
        Task<CategoryResponseDto> GetCategoryByIdAsync(int id);
        Task<List<CategoryResponseDto>> CreateCategoryAsync(CategoryDto categoryDto);
        Task<List<CategoryResponseDto>> UpdateCategoryAsync(int id, CategoryDto categoryDto);
        Task<List<CategoryResponseDto>> DeleteCategoryAsync(int id);
    }
}
