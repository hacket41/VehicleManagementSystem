using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class NotificationController(INotificationService notificationService) : ControllerBase
{
    /// <summary>Check inventory and notify admin about low-stock parts (&lt;10 units).</summary>
    [HttpPost("check-low-stock")]
    public async Task<IActionResult> CheckLowStock()
    {
        var result = await notificationService.CheckLowStockAsync();
        return Ok(result);
    }

    /// <summary>Send email reminders to customers with credit payments overdue &gt;1 month.</summary>
    [HttpPost("send-credit-reminders")]
    public async Task<IActionResult> SendCreditReminders()
    {
        var result = await notificationService.SendCreditRemindersAsync();
        return Ok(result);
    }
}
