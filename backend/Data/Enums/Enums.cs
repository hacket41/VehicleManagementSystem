namespace backend.Data.Enums;

public enum ReportType { Daily, Monthly, Yearly }
public enum CustomerReportType { TopSpenders, RegularCustomers, PendingCredits }
public enum PaymentMethod { Cash, Credit, Card, Transfer }
public enum PaymentStatus { Paid, Pending, Overdue, PartiallyPaid }
public enum PurchaseStatus { Draft, Ordered, Received, Cancelled }
public enum AppointmentStatus { Booked, Confirmed, InProgress, Completed, Cancelled }
public enum NotificationStatus { Pending, Sent, Failed }