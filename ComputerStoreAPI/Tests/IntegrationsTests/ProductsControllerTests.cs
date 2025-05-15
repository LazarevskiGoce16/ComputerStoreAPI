/*
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using ComputerStoreAPI.DTOs;

public class ProductsControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ProductsControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Post_And_Get_Product()
    {
        var catDto = new { Name = "CPU" };
        var catResp = await _client.PostAsJsonAsync("/api/categories", catDto);
        catResp.EnsureSuccessStatusCode();
        var cat = await catResp.Content.ReadFromJsonAsync<CategoryDto>();

        var prodDto = new
        {
            Name = "Intel Core i7",
            Price = 300,
            Stock = 5,
            CategoryIds = new List<int> { cat.Id }
        };
        var prodResp = await _client.PostAsJsonAsync("/api/products", prodDto);
        prodResp.EnsureSuccessStatusCode();

        var getResp = await _client.GetAsync("/api/products");
        getResp.EnsureSuccessStatusCode();
        var products = await getResp.Content.ReadFromJsonAsync<List<ProductDto>>();
        Assert.Contains(products, p => p.Name == "Intel Core i7");
    }
}
*/