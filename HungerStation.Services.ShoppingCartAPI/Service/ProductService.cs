using System.Text.Json;
using HungerStation.Services.ShoppingCartAPI.Models.Dto;
using HungerStation.Services.ShoppingCartAPI1.Service.IService;

namespace HungerStation.Services.ShoppingCartAPI1.Service;

public class ProductService : IProductService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ProductService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    public async Task<IEnumerable<ProductDto>> GetProducts()
    {
        var client = _httpClientFactory.CreateClient("Product");
        var response = await client.GetAsync("api/product");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var products = JsonSerializer.Deserialize<IEnumerable<ProductDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return products;
        }
        return null;
    }
}