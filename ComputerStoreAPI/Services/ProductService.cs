using ComputerStoreAPI.Data;
using ComputerStoreAPI.DTOs;
using ComputerStoreAPI.Entities;
using Microsoft.EntityFrameworkCore;

public class ProductService
{
    private readonly DataContext _db;
    public ProductService(DataContext db) { _db = db; }

    public async Task<List<ProductDto>> GetAllAsync()
    {
        return await _db.Products
            .Include(p => p.ProductCategories)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                CategoryIds = p.ProductCategories.Select(pc => pc.CategoryId).ToList()
            }).ToListAsync();
    }

    public async Task<ProductDto?> GetAsync(int id)
    {
        var p = await _db.Products.Include(p => p.ProductCategories)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (p == null) return null;
        return new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            Stock = p.Stock,
            CategoryIds = p.ProductCategories.Select(pc => pc.CategoryId).ToList()
        };
    }

    public async Task<ProductDto> CreateAsync(ProductDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name) || dto.Price <= 0 || dto.CategoryIds == null || !dto.CategoryIds.Any())
            throw new ArgumentException("Name, price, and at least one category are required.");

        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            Stock = dto.Stock,
            ProductCategories = dto.CategoryIds.Select(cid => new ProductCategory { CategoryId = cid }).ToList()
        };
        _db.Products.Add(product);
        await _db.SaveChangesAsync();
        dto.Id = product.Id;
        return dto;
    }

    public async Task<bool> UpdateAsync(int id, ProductDto dto)
    {
        var p = await _db.Products.Include(p => p.ProductCategories).FirstOrDefaultAsync(p => p.Id == id);
        if (p == null) return false;
        if (string.IsNullOrWhiteSpace(dto.Name) || dto.Price <= 0 || dto.CategoryIds == null || !dto.CategoryIds.Any())
            throw new ArgumentException("Name, price, and at least one category are required.");

        p.Name = dto.Name;
        p.Description = dto.Description;
        p.Price = dto.Price;
        p.Stock = dto.Stock;

        p.ProductCategories.Clear();
        foreach (var cid in dto.CategoryIds)
            p.ProductCategories.Add(new ProductCategory { ProductId = p.Id, CategoryId = cid });

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var p = await _db.Products.FindAsync(id);
        if (p == null) return false;
        _db.Products.Remove(p);
        await _db.SaveChangesAsync();
        return true;
    }
}
