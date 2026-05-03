using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class NotificationController(INotificationService notificationService) : ControllerBase
{
    
    [HttpPost("check-low-stock")]
    public async Task<IActionResult> CheckLowStock()
    {
        var result = await notificationService.CheckLowStockAsync();
        return Ok(result);
    }
    
    [HttpPost("send-credit-reminders")]
    public async Task<IActionResult> SendCreditReminders()
    {
        var result = await notificationService.SendCreditRemindersAsync();
        return Ok(result);
    }
}
