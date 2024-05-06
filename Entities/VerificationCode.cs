namespace ReminderApplication.Entities
{
    public class VerificationCode
    {
        public int Code { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
