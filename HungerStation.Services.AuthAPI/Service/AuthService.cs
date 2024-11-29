using HungerStation.Services.AuthAPI.Data;
using HungerStation.Services.AuthAPI.Models;
using HungerStation.Services.AuthAPI.Models.Dto;
using HungerStation.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace HungerStation.Services.AuthAPI.Service;

public class AuthService : IAuthService
{
    private readonly AppDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(AppDbContext db,
        UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,IJwtTokenGenerator jwtTokenGenerator)
    {
        _db = db;
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
    {
        ApplicationUser user = new()
        {
            UserName = registrationRequestDto.Email,
            Email = registrationRequestDto.Email,
            NormalizedEmail = registrationRequestDto.Email.ToUpper(),
            Name = registrationRequestDto.Name,
            PhoneNumber = registrationRequestDto.PhoneNumber
        };
        try
        {
            var result =await  _userManager.CreateAsync(user,registrationRequestDto.Password);
            if (result.Succeeded)
            {
                var userToReturn = _db.ApplicationUsers.First(u => u.UserName == registrationRequestDto.Email);
                UserDto userDto = new()
                {
                    Email = userToReturn.Email,
                    ID = userToReturn.Id,
                    Name = userToReturn.Name,
                    PhoneNumber = userToReturn.PhoneNumber
                };
                return "";
            }
            else
            {
                return result.Errors.Select(e => e.Description).FirstOrDefault();
            } 
        }
        catch (Exception ex)
        {
        }
        return "Error";
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
    {
        var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());
        bool isValid = await _userManager.CheckPasswordAsync(user,loginRequestDto.Password);
        if(user==null || isValid == false)
        {
            return new LoginResponseDto() { User = null,Token="" };
        }
        var token = _jwtTokenGenerator.GenerateToken(user);
        UserDto userDTO = new()
        {
            Email = user.Email,
            ID = user.Id,
            Name = user.Name,
            PhoneNumber = user.PhoneNumber
        };
        LoginResponseDto loginResponseDto = new LoginResponseDto()
        {
            User = userDTO,
            Token = token
        };
        return loginResponseDto;
    }

    
}