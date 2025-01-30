using HungerStation.Services.EmailAPI.Models.Dtos;

namespace HungerStation.Services.EmailAPI.Services;

public interface IEmailService
{
    Task EmailCartAndLogAsync(CartDto cartDto);
}