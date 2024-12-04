using HungerStation.Web.Models;
using HungerStation.Web.Service.IService;
using HungerStation.Web.utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace HungerStation.Web.Controllers;

public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
   
    [HttpGet]
    public IActionResult Login()
    {
        LoginRequestDto loginRequestDto = new LoginRequestDto();
        return View(loginRequestDto);
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginRequestDto obj)
    {
        ResponseDto responseDto = await _authService.LoginAsync(obj);
        if(responseDto!=null && responseDto.IsSuccess)
        {
          LoginResponseDto loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Result));
          
          return RedirectToAction("index", "home");
        }
        else
        {
            ModelState.AddModelError("", responseDto.Message);
            return View(obj);
        }
    }
    [HttpGet]
    public IActionResult Register()
    {
        var roleList = new List<SelectListItem>()
        {
            new SelectListItem{Text=SD.RoleAdmin,Value=SD.RoleAdmin},
            new SelectListItem{Text=SD.RoleCustomer,Value=SD.RoleCustomer},
        };
        ViewBag.RoleList = roleList;
        return View();
    }
    [HttpPost]
    [HttpPost]
    public async Task<IActionResult> Register(RegistrationRequestDto obj)
    {
        ResponseDto result = await _authService.RegisterAsync(obj);
        ResponseDto assignRole;
        if (result != null && result.IsSuccess)
        {
            if (string.IsNullOrEmpty(obj.Role))
            {
                obj.Role = SD.RoleCustomer;
            }
            assignRole = await _authService.AssignRoleAsync(obj);
            if (assignRole != null && assignRole.IsSuccess)
            {
                TempData["success"] = "Registration Successful";
                return RedirectToAction(nameof(Login));
            }
            else
            {
                ModelState.AddModelError("", assignRole?.Message ?? "Error assigning role.");
            }
        }
        else
        {
            ModelState.AddModelError("", result?.Message ?? "Registration failed.");
        }

        var roleList = new List<SelectListItem>()
        {
            new SelectListItem{Text=SD.RoleAdmin,Value=SD.RoleAdmin},
            new SelectListItem{Text=SD.RoleCustomer,Value=SD.RoleCustomer},
        };
        ViewBag.RoleList = roleList;
        return View(obj);
    }    [HttpGet]
    public IActionResult logout()
    {
        return View();
    }
    
    
}