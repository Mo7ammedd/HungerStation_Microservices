public class StripeRequestDto
{
    public string? StripeSessionUrl { get; set; }
    public string? StripeSessionId { get; set; }
    public string ApprovedUrl { get; set; } = string.Empty;
    public string CancelUrl { get; set; } = string.Empty;
    public OrderHeaderDto OrderHeader { get; set; } = new();
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