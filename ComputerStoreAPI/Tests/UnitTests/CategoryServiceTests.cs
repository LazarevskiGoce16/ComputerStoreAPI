using Xunit;
using Microsoft.EntityFrameworkCore;
using ComputerStoreAPI.DTOs;
using ComputerStoreAPI.Data;

public class CategoryServiceTests
{
    private DataContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "CategoryTestDb")
            .Options;
        return new DataContext(options);
    }

    [Fact]
    public async Task CreateAsync_Should_Add_Category()
    {
        var db = GetDbContext();
        var service = new CategoryService(db);

        var dto = new CategoryDto { Name = "CPU" };
        var result = await service.CreateAsync(dto);

        Assert.NotNull(result);
        Assert.True(result.Id > 0);
        Assert.Equal("CPU", result.Name);
    }

    [Fact]
    public async Task CreateAsync_Should_Throw_On_Empty_Name()
    {
        var db = GetDbContext();
        var service = new CategoryService(db);

        var dto = new CategoryDto { Name = "" };
        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateAsync(dto));
    }
}
