namespace ReminderApplication.DTOs.ResponseModels
{
    public class EventResponseModel : BaseResponse
    {
        public EventDto Data { get; set; }
    }
    public class EventsResponseModel : BaseResponse
    {
        public List<EventDto> Data { get; set; } = new List<EventDto>();
    }
}
