/*
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

public class CategoriesControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public CategoriesControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Post_And_Get_Category()
    {
        var dto = new { Name = "GPU", Description = "Graphics Cards" };
        var postResponse = await _client.PostAsJsonAsync("/api/categories", dto);
        postResponse.EnsureSuccessStatusCode();

        var getResponse = await _client.GetAsync("/api/categories");
        getResponse.EnsureSuccessStatusCode();

        var categories = await getResponse.Content.ReadFromJsonAsync<List<CategoryDto>>();
        Assert.Contains(categories, c => c.Name == "GPU");
    }
}
*/