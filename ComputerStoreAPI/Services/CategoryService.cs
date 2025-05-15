using ComputerStoreAPI.Data;
using ComputerStoreAPI.DTOs;
using ComputerStoreAPI.Entities;
using Microsoft.EntityFrameworkCore;

public class CategoryService
{
    private readonly DataContext _db;
    public CategoryService(DataContext db) { _db = db; }

    public async Task<List<CategoryDto>> GetAllAsync()
    {
        return await _db.Categories.Select(c => new CategoryDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description
        }).ToListAsync();
    }

    public async Task<CategoryDto?> GetAsync(int id)
    {
        var c = await _db.Categories.FindAsync(id);
        if (c == null) return null;
        return new CategoryDto { Id = c.Id, Name = c.Name, Description = c.Description };
    }

    public async Task<CategoryDto> CreateAsync(CategoryDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Category name is required.");
        var c = new Category { Name = dto.Name, Description = dto.Description };
        _db.Categories.Add(c);
        await _db.SaveChangesAsync();
        dto.Id = c.Id;
        return dto;
    }

    public async Task<bool> UpdateAsync(int id, CategoryDto dto)
    {
        var c = await _db.Categories.FindAsync(id);
        if (c == null) return false;
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Category name is required.");
        c.Name = dto.Name;
        c.Description = dto.Description;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var c = await _db.Categories.FindAsync(id);
        if (c == null) return false;
        _db.Categories.Remove(c);
        await _db.SaveChangesAsync();
        return true;
    }
}
