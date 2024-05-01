using ReminderApplication.DTOs.RequestModels;
using ReminderApplication.DTOs.ResponseModels;
using ReminderApplication.DTOs;
using ReminderApplication.Entities;
using ReminderApplication.Implementations.Repositories;
using ReminderApplication.Interfaces.Repositories;
using ReminderApplication.Interfaces.Services;
using ReminderApplication.Entities.Enums;

namespace ReminderApplication.Implementations.Services
{
    public class ReminderService : IReminderService
    {
        private readonly IReminderRepository _reminderRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;

        public ReminderService(IReminderRepository reminderRepository, IEventRepository eventRepository, IUserRepository userRepository)
        {
            _reminderRepository = reminderRepository;
            _eventRepository = eventRepository;
            _userRepository = userRepository;
        }
        public async Task<BaseResponse> CreateReminderAsync(CreateReminderRequestModel model)
        {
            var reminder = await _eventRepository.GetAsync(r => r.Equals(model.EventID) && r.Equals(model.UserID));
            if (reminder != null)
            {
                return new BaseResponse()
                {
                    Message = $"Reminders has already been created for {model.UserName} event {model.EventID}",
                    Success = false,
                };
            }
            var reminders = new Reminder
            {
                EventID = model.EventID,
                UserID = model.UserID,
                UserName = model.UserName,
                PhoneNumer = model.PhoneNumber,
                ReminderDateTime = model.ReminderDateTime,
                EventTitle = model.EventTitle,
                EventDate = model.EventDate,
                Status = model.Status,


            };
            await _reminderRepository.CreateAsync(reminders);
            return new BaseResponse()
            {
                Message = $"You have successfully created a reminder for {model.UserName}/'s event.",
                Success = true,
            };
        }
        public async Task<BaseResponse> UpdateReminderAsync(UpdateReminderRequestModel model)
        {
            var reminder = await _reminderRepository.GetAsync(r => r.Equals(model.EventId) && r.Equals(model.UserId));
            if (reminder == null)
            {
                return new BaseResponse
                {
                    Message = "No Reminder created for this event",
                    Success = false,
                };
            }
            var _reminder = new Reminder
            {
                Status = model.Status,
                EventDate = model.EventDate,
            };
            await _reminderRepository.UpdateAsync(_reminder);
            return new BaseResponse()
            {
                Message = "Event date has changed and as a result, Reminder has been updated successfully!",
                Success = true,
            };
        }
        public async Task<BaseResponse> TerminateReminderAsync(int EventId,EventStatus Status)
        {
            var toTerminate = await _reminderRepository.GetAsync(t => t.Id == EventId && t.Status == EventStatus.Cancelled);
            if (toTerminate == null)
            {
                return new BaseResponse()
                {
                    Message = "No Reminders created for this event",
                    Success = false,

                };
            }
            toTerminate.IsDeleted = true;
            await _reminderRepository.UpdateAsync(toTerminate);
            return new BaseResponse()
            {
                Message = "Reminders deleted successfully",
                Success = true,
            };
        }
        public async Task<RemindersResponseModel> GetRemindersByEventIdAsync(int EventId)
        {
            var reminders = await _reminderRepository.GetRemindersByEventId(EventId);
            if (reminders == null)
            {
                return new RemindersResponseModel()
                {
                    Message = $"No Reminders for the {EventId}",
                    Success = false,
                };
            }
            return new RemindersResponseModel
            {
                Data = reminders.Select(reminders => new ReminderDto
                {
                    ReminderID = reminders.Id,
                    UserID = reminders.UserID,
                    ReminderDateTime = reminders.ReminderDateTime,

                }).ToList(),
                Message = "Reminders found successfully",
                Success = true,
            };

        }
        public async Task<RemindersResponseModel> GetRemindersToDisplayAsync()
        {
            var toDisplayReminder = await _reminderRepository.GetRemindersToDisplayAsync();
            if (toDisplayReminder.Count == 0)
            {
                return new RemindersResponseModel()
                {
                    Message = "No Reminders created for such event!",
                    Success = false,
                };
            }
            return new RemindersResponseModel()
            {
                Data = toDisplayReminder.Select(t => new ReminderDto
                {
                    EventID = t.Id,
                    UserID = t.UserID,
                    ReminderID = t.ReminderID,
                    UserName = t.UserName,
                }).ToList(),
            };
        }
    }
}
