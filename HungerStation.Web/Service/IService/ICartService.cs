using HungerStation.Web.Models;

namespace HungerStation.Web.Service.IService;

public interface ICartService 
{
    Task<ResponseDto?> GetCartByUSerIdAsync(string userId);
    Task<ResponseDto?> UpsertCartAsync(CartDto cartDto);
    Task<ResponseDto?> RemoveFromCartAsync(int cartDetailsId);
    Task<ResponseDto?> ApplyCouponAsync(CartDto cartDto);
 
}