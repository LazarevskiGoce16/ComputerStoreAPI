using ComputerStoreAPI.Data;
using ComputerStoreAPI.DTOs;
using Microsoft.EntityFrameworkCore;

public class DiscountService
{
    private readonly DataContext _db;
    public DiscountService(DataContext db) { _db = db; }

    public async Task<BasketDiscountResultDto> CalculateDiscountAsync(List<BasketItemDto> basket)
    {
        var result = new BasketDiscountResultDto();
        if (basket == null || !basket.Any())
        {
            result.Error = "Basket is empty.";
            return result;
        }

        var productIds = basket.Select(b => b.ProductId).ToList();
        var products = await _db.Products
            .Include(p => p.ProductCategories).ThenInclude(pc => pc.Category)
            .Where(p => productIds.Contains(p.Id))
            .ToListAsync();

        foreach (var item in basket)
        {
            var prod = products.FirstOrDefault(p => p.Id == item.ProductId);
            if (prod == null)
            {
                result.Error = $"Product with ID {item.ProductId} not found.";
                return result;
            }
            if (prod.Stock < item.Quantity)
            {
                result.Error = $"Not enough stock for {prod.Name}.";
                return result;
            }
        }

        decimal total = 0;
        decimal discount = 0;

        var categoryProductCount = new Dictionary<int, int>();

        foreach (var item in basket)
        {
            var prod = products.First(p => p.Id == item.ProductId);
            total += prod.Price * item.Quantity;

            foreach (var pc in prod.ProductCategories)
            {
                if (!categoryProductCount.ContainsKey(pc.CategoryId))
                    categoryProductCount[pc.CategoryId] = 0;
                categoryProductCount[pc.CategoryId] += item.Quantity;
            }
        }

        foreach (var item in basket)
        {
            var prod = products.First(p => p.Id == item.ProductId);
            foreach (var pc in prod.ProductCategories)
            {
                if (categoryProductCount[pc.CategoryId] > 1)
                {
                    discount += prod.Price * 0.05m;
                    break;
                }
            }
        }

        if (basket.Count == 1 && basket[0].Quantity == 1)
            discount = 0;

        result.TotalPrice = total - discount;
        result.DiscountApplied = discount;
        return result;
    }
}
