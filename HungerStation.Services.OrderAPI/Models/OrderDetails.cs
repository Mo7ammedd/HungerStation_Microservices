using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HungerStation.Services.OrderAPI.Models;

public class OrderDetails
{
    [Key]
    public int OrderDetailsId { get; set; }
    public int OrderHeaderId { get; set; }
    [ForeignKey("OrderHeaderId")]
    public OrderHeader? OrderHeader { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public double Price { get; set; }
    public int Count { get; set; }
}
