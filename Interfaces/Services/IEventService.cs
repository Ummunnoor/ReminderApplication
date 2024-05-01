using ReminderApplication.DTOs.RequestModels;
using ReminderApplication.DTOs.ResponseModels;

namespace ReminderApplication.Interfaces.Services
{
    public interface IEventService
    {
        Task<BaseResponse> CreateEventAsync(int UserId, CreateEventRequestModel model);
        Task<BaseResponse> UpdateEventAsync(UpdateEventRequestModel model);
        Task<BaseResponse> CancelEventAsync(int id);
        Task<EventsResponseModel> GetAllEventsAsync();
        Task<EventsResponseModel> GetEventsToDisplayAsync(int UserId);
        Task<EventsResponseModel> GetEventsByDateAsync(DateTime date);
        Task<EventResponseModel> GetEventByIdAsync(int id);
    }
}
