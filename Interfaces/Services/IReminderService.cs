using ReminderApplication.DTOs.RequestModels;
using ReminderApplication.DTOs.ResponseModels;
using ReminderApplication.Entities.Enums;

namespace ReminderApplication.Interfaces.Services
{
    public interface IReminderService
    {
        Task<BaseResponse> CreateReminderAsync(CreateReminderRequestModel model);
        Task<BaseResponse> UpdateReminderAsync(UpdateReminderRequestModel model);
        Task<BaseResponse> TerminateReminderAsync(int EventId,EventStatus Status);
        Task<RemindersResponseModel> GetRemindersByEventIdAsync(int EventId);
        Task<RemindersResponseModel> GetRemindersToDisplayAsync();
    }
}
