using HungerStation.Services.AuthAPI.Models;
using HungerStation.Services.AuthAPI.Models.Dto;
using HungerStation.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HungerStation.Services.AuthAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    protected ResponseDto _response;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
        _response = new();
    }



    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
    {

        var errorMessage = await _authService.Register(model);
        if (!string.IsNullOrEmpty(errorMessage))
        {
            _response.IsSuccess = false;
            _response.Message= errorMessage;
            return BadRequest(_response);
        }
        return Ok(_response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
    {
        var loginResponse = await _authService.Login(model);
        if (loginResponse.User == null)
        {
            _response.IsSuccess = false;
            _response.Message = "Username or password is incorrect";
            return BadRequest(_response);
        }
        _response.Result = loginResponse;
        return Ok(_response);
    }
  
    
}