using System.ComponentModel.DataAnnotations;

namespace HungerStation.Services.RewardAPI.Models;

public class Rewards
{
    [Key]
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public DateTime RewardsDate { get; set; }
    public int RewardsActivity { get; set; }
    public int OrderId { get; set; }
}
