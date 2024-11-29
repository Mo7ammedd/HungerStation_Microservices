using Microsoft.AspNetCore.Identity;

namespace HungerStation.Services.AuthAPI.Models;

public class ApplicationUser : IdentityUser
{
    
    public string Name { get; set; }
    
    
}