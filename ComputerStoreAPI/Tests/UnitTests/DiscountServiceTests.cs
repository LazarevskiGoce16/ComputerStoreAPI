using Xunit;
using Microsoft.EntityFrameworkCore;
using ComputerStoreAPI.DTOs;
using ComputerStoreAPI.Entities;
using ComputerStoreAPI.Data;

public class DiscountServiceTests
{
    private DataContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var db = new DataContext(options);

        var cat = new Category { Id = 1, Name = "CPU" };
        var prod = new Product
        {
            Id = 1,
            Name = "Intel Core i9",
            Price = 500,
            Stock = 2,
            ProductCategories = new List<ProductCategory>
            {
                new ProductCategory { CategoryId = 1 }
            }
        };
        db.Categories.Add(cat);
        db.Products.Add(prod);
        db.SaveChanges();
        return db;
    }

    [Fact]
    public async Task CalculateDiscountAsync_Should_Apply_No_Discount_For_Single_Product()
    {
        var db = GetDbContext();
        var service = new DiscountService(db);

        var basket = new List<BasketItemDto>
        {
            new BasketItemDto { ProductId = 1, Quantity = 1 }
        };

        var result = await service.CalculateDiscountAsync(basket);

        Assert.Equal(500, result.TotalPrice);
        Assert.Equal(0, result.DiscountApplied);
        Assert.Null(result.Error);
    }

    [Fact]
    public async Task CalculateDiscountAsync_Should_Apply_5Percent_Discount()
    {
        var db = GetDbContext();
        var service = new DiscountService(db);

        var basket = new List<BasketItemDto>
        {
            new BasketItemDto { ProductId = 1, Quantity = 2 }
        };

        var result = await service.CalculateDiscountAsync(basket);

        Assert.Equal(500 * 2 - 500 * 0.05m, result.TotalPrice);
        Assert.Equal(500 * 0.05m, result.DiscountApplied);
        Assert.Null(result.Error);
    }

    [Fact]
    public async Task CalculateDiscountAsync_Should_Return_Error_On_Insufficient_Stock()
    {
        var db = GetDbContext();
        var service = new DiscountService(db);

        var basket = new List<BasketItemDto>
        {
            new BasketItemDto { ProductId = 1, Quantity = 3 }
        };

        var result = await service.CalculateDiscountAsync(basket);

        Assert.NotNull(result.Error);
        Assert.Contains("Not enough stock", result.Error);
    }
}
