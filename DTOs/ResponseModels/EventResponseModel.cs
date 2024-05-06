namespace ReminderApplication.DTOs.ResponseModels
{
    public class EventResponseModel : BaseResponse<EventDto>
    {
        public EventDto Data { get; set; }
    }
    public class EventsResponseModel : BaseResponse<List<EventDto>>
    {
        public List<EventDto> Data { get; set; } = new List<EventDto>();
    }
}
