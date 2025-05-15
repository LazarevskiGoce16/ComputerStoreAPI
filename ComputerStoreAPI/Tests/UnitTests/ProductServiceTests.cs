using Xunit;
using Microsoft.EntityFrameworkCore;
using ComputerStoreAPI.DTOs;
using ComputerStoreAPI.Entities;
using ComputerStoreAPI.Data;

public class ProductServiceTests
{
    private DataContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var db = new DataContext(options);
        db.Categories.Add(new Category { Id = 1, Name = "CPU" });
        db.SaveChanges();
        return db;
    }

    [Fact]
    public async Task CreateAsync_Should_Add_Product()
    {
        var db = GetDbContext();
        var service = new ProductService(db);

        var dto = new ProductDto
        {
            Name = "Intel Core i9",
            Description = "High-end CPU",
            Price = 499.99m,
            Stock = 10,
            CategoryIds = new List<int> { 1 }
        };

        var result = await service.CreateAsync(dto);

        Assert.NotNull(result);
        Assert.True(result.Id > 0);
        Assert.Equal("Intel Core i9", result.Name);
    }

    [Fact]
    public async Task CreateAsync_Should_Throw_On_Missing_Category()
    {
        var db = GetDbContext();
        var service = new ProductService(db);

        var dto = new ProductDto
        {
            Name = "Intel Core i9",
            Price = 499.99m,
            Stock = 10,
            CategoryIds = new List<int>()
        };

        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateAsync(dto));
    }
}
