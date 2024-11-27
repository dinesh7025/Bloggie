namespace Bloggie.web.Models.ViewModels
{
    public class Notification
    {
        public string Message { get; set; } 

        public NotificationType Type { get; set; }
    }

    public enum NotificationType
    {
        Success,
        Info,
        Error
    }
}
