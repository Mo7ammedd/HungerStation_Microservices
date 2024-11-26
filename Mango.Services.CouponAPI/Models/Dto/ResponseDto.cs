namespace Mango.Services.CouponAPI.Models.Dtos;

public class ResponseDto
{
    public object Result { get; set; }
    public bool IsSuccess { get; set; } = true;
    public string DisplayMessage { get; set; } = "";
    
}