namespace backend.Services.Interfaces;
using backend.Data.DTO.Request;
public interface INotificationService
{
    Task<NotificationSummaryDto> CheckLowStockAsync();
    Task<NotificationSummaryDto> SendCreditRemindersAsync();
}