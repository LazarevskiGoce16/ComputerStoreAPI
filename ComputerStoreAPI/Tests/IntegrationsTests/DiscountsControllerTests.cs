/*
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using ComputerStoreAPI.DTOs;

public class DiscountControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public DiscountControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CalculateDiscount_Returns_Correct_Discount()
    {
        var catDto = new { Name = "CPU" };
        var catResp = await _client.PostAsJsonAsync("/api/categories", catDto);
        var cat = await catResp.Content.ReadFromJsonAsync<CategoryDto>();

        var prodDto = new
        {
            Name = "Intel Core i9",
            Price = 500,
            Stock = 10,
            CategoryIds = new List<int> { cat.Id }
        };
        var prodResp = await _client.PostAsJsonAsync("/api/products", prodDto);
        var prod = await prodResp.Content.ReadFromJsonAsync<ProductDto>();

        var basket = new[]
        {
            new { ProductId = prod.Id, Quantity = 2 }
        };
        var discountResp = await _client.PostAsJsonAsync("/api/discount", basket);
        discountResp.EnsureSuccessStatusCode();

        var discountResult = await discountResp.Content.ReadFromJsonAsync<BasketDiscountResultDto>();
        Assert.Equal(500 * 2 - 500 * 0.05m, discountResult.TotalPrice);
        Assert.Equal(500 * 0.05m, discountResult.DiscountApplied);
    }
}
*/