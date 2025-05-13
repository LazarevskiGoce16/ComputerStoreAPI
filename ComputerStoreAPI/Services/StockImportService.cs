using ComputerStoreAPI.DTOs;
using ComputerStoreAPI.Entities;
using ComputerStoreAPI.Services;

public class StockImportService : IStockImportService
{
    private readonly IApplicationDbContext _context;

    public StockImportService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task ImportStockAsync(List<ProductStockDto> stockItems)
    {
        foreach (var item in stockItems)
        {
            var product = await _context.Products
                .Include(p => p.Categories)
                .FirstOrDefaultAsync(p => p.Name == item.Name);

            var categories = new List<Category>();
            foreach (var catName in item.Categories)
            {
                var trimmed = catName.Trim();
                var category = await _context.Categories
                    .FirstOrDefaultAsync(c => c.Name == trimmed);

                if (category == null)
                {
                    category = new Category { Name = trimmed };
                    _context.Categories.Add(category);
                }

                categories.Add(category);
            }

            if (product == null)
            {
                product = new Product
                {
                    Name = item.Name,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    Categories = categories
                };
                _context.Products.Add(product);
            }
            else
            {
                product.Quantity += item.Quantity;
                product.Price = item.Price;
                product.Categories = categories;
            }
        }

        await _context.SaveChangesAsync();
    }
}
