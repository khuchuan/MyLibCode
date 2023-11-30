namespace DTO
{
    public class Notification
    {
        public string EmailTitle { get; set; }
        public string Message { get; set; }
    }

    public static class NotifyMode
    {
        public const int Disable = 0;
        public const int Email = 1;
    }
}
