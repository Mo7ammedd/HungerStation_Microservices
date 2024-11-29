using HungerStation.Services.AuthAPI.Models;

namespace HungerStation.Services.AuthAPI.Service.IService;

public interface IJwtTokenGenerator
{
    
     string GenerateToken(ApplicationUser applicationUser);
}