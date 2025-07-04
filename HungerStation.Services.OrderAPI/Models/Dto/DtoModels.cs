namespace HungerStation.Services.OrderAPI.Models.Dto;

public class ResponseDto
{
    public object? Result { get; set; }
    public bool IsSuccess { get; set; } = true;
    public string Message { get; set; } = "";
}

public class OrderHeaderDto
{
    public int OrderHeaderId { get; set; }
    public string? UserId { get; set; }
    public string? CouponCode { get; set; }
    public double Discount { get; set; }
    public double OrderTotal { get; set; }
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public DateTime OrderTime { get; set; }
    public string? Status { get; set; }
    public string? PaymentIntentId { get; set; }
    public string? StripeSessionId { get; set; }
    public IEnumerable<OrderDetailsDto>? OrderDetails { get; set; }
}

public class OrderDetailsDto
{
    public int OrderDetailsId { get; set; }
    public int OrderHeaderId { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public double Price { get; set; }
    public int Count { get; set; }
}

public class CartDto
{
    public CartHeaderDto CartHeader { get; set; } = new();
    public IEnumerable<CartDetailsDto>? CartDetails { get; set; }
}

public class CartHeaderDto
{
    public int CartHeaderId { get; set; }
    public string? UserId { get; set; }
    public string? CouponCode { get; set; }
    public double Discount { get; set; }
    public double CartTotal { get; set; }
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
}

public class CartDetailsDto
{
    public int CartDetailsId { get; set; }
    public int CartHeaderId { get; set; }
    public int ProductId { get; set; }
    public ProductDto? Product { get; set; }
    public int Count { get; set; }
}

public class ProductDto
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
}

public class StripeRequestDto
{
    public string? StripeSessionUrl { get; set; }
    public string? StripeSessionId { get; set; }
    public string ApprovedUrl { get; set; } = string.Empty;
    public string CancelUrl { get; set; } = string.Empty;
    public OrderHeaderDto OrderHeader { get; set; } = new();
}

public class RequestDto
{
    public string ApiType { get; set; } = "GET";
    public string Url { get; set; } = string.Empty;
    public object? Data { get; set; }
    public string AccessToken { get; set; } = string.Empty;
}

public class RewardsDto
{
    public string UserId { get; set; } = string.Empty;
    public int RewardsActivity { get; set; }
    public int OrderId { get; set; }
}
