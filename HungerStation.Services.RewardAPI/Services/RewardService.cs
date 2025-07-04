using HungerStation.Services.RewardAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace HungerStation.Services.RewardAPI.Services;

public class RewardService : IRewardService
{
    private DbContextOptions<AppDbContext> _dbOptions;

    public RewardService(DbContextOptions<AppDbContext> dbOptions)
    {
        _dbOptions = dbOptions;
    }

    public async Task UpdateRewards(RewardsMessage rewardsMessage)
    {
        try
        {
            var rewards = new Models.Rewards()
            {
                OrderId = rewardsMessage.OrderId,
                RewardsActivity = rewardsMessage.RewardsActivity,
                UserId = rewardsMessage.UserId,
                RewardsDate = DateTime.Now
            };

            await using var _db = new AppDbContext(_dbOptions);
            await _db.Rewards.AddAsync(rewards);
            await _db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating rewards: {ex.Message}");
        }
    }
}

public interface IRewardService
{
    Task UpdateRewards(RewardsMessage rewardsMessage);
}

public class RewardsMessage
{
    public string UserId { get; set; } = string.Empty;
    public int RewardsActivity { get; set; }
    public int OrderId { get; set; }
}
