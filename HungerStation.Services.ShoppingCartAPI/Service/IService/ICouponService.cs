using HungerStation.Services.ShoppingCartAPI.Models.Dto;

namespace HungerStation.Services.ShoppingCartAPI.Service.IService
{
    public interface ICouponService
    {
        Task<CouponDto> GetCoupon(string couponCode);
    }
}
