using AutoMapper;
using ComputerStoreAPI.Data;
using ComputerStoreAPI.DTOs;
using ComputerStoreAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComputerStoreAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CategoryService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CategoryResponseDto>> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return _mapper.Map<List<CategoryResponseDto>>(categories);
        }

        public async Task<CategoryResponseDto?> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            return category == null ? null : _mapper.Map<CategoryResponseDto>(category);
        }

        public async Task<List<CategoryResponseDto>> CreateCategoryAsync(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return await GetAllCategoriesAsync();
        }

        public async Task<List<CategoryResponseDto>> UpdateCategoryAsync(int id, CategoryDto categoryDto)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return null;

            _mapper.Map(categoryDto, category);
            await _context.SaveChangesAsync();

            return await GetAllCategoriesAsync();
        }

        public async Task<List<CategoryResponseDto>> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return null;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return await GetAllCategoriesAsync();
        }
    }
}
