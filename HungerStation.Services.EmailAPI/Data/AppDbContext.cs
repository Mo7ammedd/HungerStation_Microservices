using HungerStation.Services.EmailAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HungerStation.Services.EmailAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<EmailLogger> EmailLoggers { get; set; }
}