using HungerStation.Services.ShoppingCartAPI.Models.Dto;
using HungerStation.Services.ShoppingCartAPI1.Service.IService;
using Newtonsoft.Json;

namespace HungerStation.Services.ShoppingCartAPI1.Service;

public class CouponService : ICouponService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CouponService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    public async Task<CouponDto> GetCoupon(string couponCode)
    {
        var client = _httpClientFactory.CreateClient("Coupon");
        var response = await client.GetAsync($"/api/coupon/GetByCode/{couponCode}");
        var apiContet = await response.Content.ReadAsStringAsync();
        var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContet);
        if (resp.IsSuccess)
        {
            return JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(resp.Result));
        }
        return new CouponDto();
    }
}