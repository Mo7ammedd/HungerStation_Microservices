using HungerStation.Services.AuthAPI.Models.Dto;

namespace HungerStation.Services.AuthAPI.Service.IService;

public interface IAuthService
{
    
    Task<string> Register(RegistrationRequestDto registrationRequestDto);
    Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    Task<bool> AssignRole(string email, string role);
    
}