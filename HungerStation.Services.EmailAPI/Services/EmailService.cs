using System.Text;
using HungerStation.Services.EmailAPI.Data;
using HungerStation.Services.EmailAPI.Models;
using HungerStation.Services.EmailAPI.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace HungerStation.Services.EmailAPI.Services;

public class EmailService : IEmailService
{
    private readonly DbContextOptions<AppDbContext> _dbOptions;

    public EmailService(DbContextOptions<AppDbContext> dbOptions)
    {
        _dbOptions = dbOptions;
    }


    public async Task EmailCartAndLogAsync(CartDto cartDto)
    {
        StringBuilder message = new StringBuilder();
        message.AppendLine("<br/>Cart Email Requested ");
        message.AppendLine("<br/>Total " + cartDto.CartHeader.CartTotal);
        message.Append("<br/>");
        message.Append("<ul>");
        foreach (var item in cartDto.CartDetails)
        {
            message.Append("<li>");
            message.Append(item.Product.Name + " x " + item.Count);
            message.Append("</li>");
        }
        message.Append("</ul>");
        await LogAndEmail(message.ToString(), cartDto.CartHeader.Email);
    }
    private async Task<bool> LogAndEmail(string message, string email)
    {
        try
        {
            EmailLogger emailLog = new()
            {
                Email = email,
                EmailSent = DateTime.Now,
                Message = message
            };
            await using var _db = new AppDbContext(_dbOptions);
            await _db.EmailLoggers.AddAsync(emailLog);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}