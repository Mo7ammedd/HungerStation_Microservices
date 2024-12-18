using HungerStation.Services.ShoppingCartAPI.Models.Dto;

namespace HungerStation.Services.ShoppingCartAPI1.Service.IService;

public interface ICouponService
{
    Task<CouponDto> GetCoupon(string couponCode);
}