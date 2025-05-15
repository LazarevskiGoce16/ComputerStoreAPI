using ComputerStoreAPI.Data;
using ComputerStoreAPI.DTOs;
using ComputerStoreAPI.Entities;
using Microsoft.EntityFrameworkCore;

public class StockImportService
{
    private readonly DataContext _db;
    public StockImportService(DataContext db) { _db = db; }

    public async Task ImportAsync(List<StockImportDto> importList)
    {
        foreach (var item in importList)
        {
            var categoryEntities = new List<Category>();
            foreach (var cname in item.Categories)
            {
                var cat = await _db.Categories.FirstOrDefaultAsync(c => c.Name == cname.Trim());
                if (cat == null)
                {
                    cat = new Category { Name = cname.Trim() };
                    _db.Categories.Add(cat);
                    await _db.SaveChangesAsync();
                }
                categoryEntities.Add(cat);
            }

            var product = await _db.Products
                .Include(p => p.ProductCategories)
                .FirstOrDefaultAsync(p => p.Name == item.Name);
            if (product == null)
            {
                product = new Product
                {
                    Name = item.Name,
                    Price = item.Price,
                    Stock = item.Quantity,
                    ProductCategories = categoryEntities.Select(c => new ProductCategory { Category = c }).ToList()
                };
                _db.Products.Add(product);
            }
            else
            {
                product.Stock += item.Quantity;
                product.Price = item.Price;
                foreach (var cat in categoryEntities)
                {
                    if (!product.ProductCategories.Any(pc => pc.CategoryId == cat.Id))
                        product.ProductCategories.Add(new ProductCategory { Product = product, Category = cat });
                }
            }
            await _db.SaveChangesAsync();
        }
    }
}
