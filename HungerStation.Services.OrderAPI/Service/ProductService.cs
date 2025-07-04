using HungerStation.Services.OrderAPI.Models.Dto;
using HungerStation.Services.OrderAPI.Service.IService;
using Newtonsoft.Json;

namespace HungerStation.Services.OrderAPI.Service;

public class ProductService : IProductService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ProductService(IHttpClientFactory clientFactory)
    {
        _httpClientFactory = clientFactory;
    }

    public async Task<IEnumerable<ProductDto>> GetProducts()
    {
        var client = _httpClientFactory.CreateClient("Product");
        var response = await client.GetAsync("/api/product");
        var apiContent = await response.Content.ReadAsStringAsync();
        var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
        if (resp != null && resp.IsSuccess)
        {
            return JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(Convert.ToString(resp.Result)!) ?? new List<ProductDto>();
        }
        return new List<ProductDto>();
    }
}
