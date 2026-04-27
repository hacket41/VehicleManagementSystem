namespace backend.Data.DTO.Request;

public class NotificationSummaryDto
{
    public int Processed { get; set; }
    public int AlreadyNotified { get; set; }
    public List<string> Details { get; set; } = new();
}