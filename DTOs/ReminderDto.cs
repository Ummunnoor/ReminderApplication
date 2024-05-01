namespace ReminderApplication.DTOs
{
    public class ReminderDto
    {
        public int ReminderID { get; set; }
        public int EventID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public DateTime ReminderDateTime { get; set; }
        public string Notification { get; set; }
    }
}

