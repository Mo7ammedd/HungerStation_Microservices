using HungerStation.Web.Service.IService;
using HungerStation.Web.utility;

namespace HungerStation.Web.Service;

public class TokenProvider : ITokenProvider
{
    private readonly IHttpContextAccessor _contextAccessor;

    public TokenProvider(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public void SetToken(string token)
    {
        _contextAccessor.HttpContext.Response.Cookies.Append(SD.TokenCookie, token);
    }

    public string? GetToken()
    {
        string? token = null;
        bool? hasToken = _contextAccessor.HttpContext.Request.Cookies.TryGetValue(SD.TokenCookie, out token);
        return hasToken == true ? token : null;
    }

    public void ClearToken()
    {
        _contextAccessor.HttpContext.Response.Cookies.Delete(SD.TokenCookie);
    }
}