namespace ReminderApplication.DTOs.RequestModels
{
    public class CreateEventRequestModel
    {
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}
