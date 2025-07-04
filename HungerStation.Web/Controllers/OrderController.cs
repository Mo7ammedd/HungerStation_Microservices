using HungerStation.Web.Models;
using HungerStation.Web.Models.Dto;
using HungerStation.Web.Service.IService;
using HungerStation.Web.utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace HungerStation.Web.Controllers;

public class OrderController : Controller
{
    private readonly IOrderService _orderService;
    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [Authorize]
    public IActionResult OrderIndex()
    {
        return View();
    }

    [Authorize]
    public async Task<IActionResult> OrderDetail(int orderId)
    {
        OrderHeaderDto orderHeaderDto = new OrderHeaderDto();
        string userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value!;

        var response = await _orderService.GetOrder(orderId);
        if (response != null && response.IsSuccess)
        {
            orderHeaderDto = JsonConvert.DeserializeObject<OrderHeaderDto>(Convert.ToString(response.Result)!)!;
        }
        if (!User.IsInRole(SD.RoleAdmin) && userId != orderHeaderDto.UserId)
        {
            return NotFound();
        }
        return View(orderHeaderDto);
    }

    [HttpPost("OrderReadyForPickup")]
    public async Task<IActionResult> OrderReadyForPickup(int orderId)
    {
        var response = await _orderService.UpdateOrderStatus(orderId, SD.Status_ReadyForPickup);
        if (response != null && response.IsSuccess)
        {
            TempData["success"] = "Status updated successfully";
        }
        return RedirectToAction(nameof(OrderDetail), new { orderId = orderId });
    }

    [HttpPost("CompleteOrder")]
    public async Task<IActionResult> CompleteOrder(int orderId)
    {
        var response = await _orderService.UpdateOrderStatus(orderId, SD.Status_Completed);
        if (response != null && response.IsSuccess)
        {
            TempData["success"] = "Status updated successfully";
        }
        return RedirectToAction(nameof(OrderDetail), new { orderId = orderId });
    }

    [HttpPost("CancelOrder")]
    public async Task<IActionResult> CancelOrder(int orderId)
    {
        var response = await _orderService.UpdateOrderStatus(orderId, SD.Status_Cancelled);
        if (response != null && response.IsSuccess)
        {
            TempData["success"] = "Status updated successfully";
        }
        return RedirectToAction(nameof(OrderDetail), new { orderId = orderId });
    }

    [HttpGet]
    public IActionResult GetAll(string status)
    {
        IEnumerable<OrderHeaderDto> list;
        string userId = "";
        if (!User.IsInRole(SD.RoleAdmin))
        {
            userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value!;
        }
        ResponseDto response = _orderService.GetAllOrder(userId).GetAwaiter().GetResult()!;
        if (response != null && response.IsSuccess)
        {
            list = JsonConvert.DeserializeObject<List<OrderHeaderDto>>(Convert.ToString(response.Result)!)!;
            switch (status)
            {
                case "approved":
                    list = list.Where(u => u.Status == SD.Status_Approved);
                    break;
                case "readyforpickup":
                    list = list.Where(u => u.Status == SD.Status_ReadyForPickup);
                    break;
                case "cancelled":
                    list = list.Where(u => u.Status == SD.Status_Cancelled);
                    break;
                default:
                    break;
            }
        }
        else
        {
            list = new List<OrderHeaderDto>();
        }
        return Json(new { data = list });
    }
}
