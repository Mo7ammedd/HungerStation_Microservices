using HungerStation.Web.Models;

namespace HungerStation.Web.Service.IService;

public interface IBaseService
{
   Task<ResponseDto?> SendAsync(RequestDto requestDto);
} 