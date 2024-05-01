namespace ReminderApplication.DTOs.RequestModels
{
    public class UpdateEventRequestModel
    {
        public int EventId { get; set; }
        public string Location { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}
