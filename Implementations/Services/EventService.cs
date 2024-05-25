using ReminderApplication.DTOs.RequestModels;
using ReminderApplication.DTOs.ResponseModels;
using ReminderApplication.DTOs;
using ReminderApplication.Entities;
using ReminderApplication.Implementations.Repositories;
using ReminderApplication.Interfaces.Services;
using ReminderApplication.Interfaces.Repositories;
using ReminderApplication.SmsServices;
using ReminderApplication.EmailServices;

namespace ReminderApplication.Implementations.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMailServices _mailService;
        private readonly ISmsService _smsService;
        public EventService(IEventRepository eventrepository, IMailServices mailService, ISmsService smsService, IUserRepository userRepository)
        {
            _eventRepository = eventrepository;
            _mailService = mailService;
            _smsService = smsService;
            _userRepository = userRepository;
        }
        public async Task<BaseResponse> CreateEventAsync(int UserId, CreateEventRequestModel model)
        {
            var user = await _userRepository.GetAsync(UserId);
            var _event = await _eventRepository.GetAsync(x => x.Id == UserId);
            if (_event != null)
            {
                return new BaseResponse
                {
                    Message = "Event has been already created",
                    Success = false

                };
            }
            var @event = new Event
            {
                UserName = model.UserName,
                Title = model.Title,
                Location = model.Location,
                StartDateTime = model.StartDateTime,
                EndDateTime = model.EndDateTime,
                Description = model.Description,
                User = user,
                UserId = UserId,
            };

            await _eventRepository.CreateAsync(@event);
            var mailRequest = new MailRequest
            {
                Subject = "Welcome To Reminder Application",
                ToEmail = @event.User.Email,
                ToName = @event.User.Email,
                HtmlContent = $"<html><body><h1>Hello {@event.User.Username}, You have a {@event.Title} scheduled  on {@event.StartDateTime} which is to end on {@event.EndDateTime}",           
            };  
            _mailService.SendEMailAsync(mailRequest);
            var smsRequest = new SmsRequest
            {
                Recipient = @event.User.PhoneNumber,
                Content = $" Dear {@event.User.Username},You have a {@event.Title} scheduled  on {@event.StartDateTime} which is to end on {@event.EndDateTime}",
            };
            _smsService.SendSmsAsync(smsRequest);
            return new BaseResponse
            {
                Message = "Event created successfully",
                Success = true
            };

        }
        public async Task<BaseResponse> UpdateEventAsync(UpdateEventRequestModel model)
        {
            var _event = await _eventRepository.GetAsync(e => e.Id == model.EventId);
            if (_event == null)
            {
                return new BaseResponse
                {
                    Message = "Event not found",
                    Success = false
                };
            }
            var @event = new Event
            {
                Location = model.Location,
                StartDateTime = model.StartDateTime,
                EndDateTime = model.EndDateTime,
            };
            return new BaseResponse
            {
                Message = "Event updated successfully",
                Success = true
            };
        }
        public async Task<BaseResponse> CancelEventAsync(int id)
        {
            var eventEntity = await _eventRepository.GetAsync(id);
            if (eventEntity == null)
            {
                return new BaseResponse
                {
                    Message = "The event does not exist",
                    Success = false,
                };
            }
            await _eventRepository.DeleteAsync(eventEntity);
            return new BaseResponse
            {
                Message = "Event deleted successfully",
                Success = true,
            };
        }
        public async Task<EventsResponseModel> GetAllEventsAsync()
        {
            var _event = await _eventRepository.GetAllAsync();
            var eventList = _event.Select(x => new EventDto()
            {
                Id = x.Id,
                Title = x.Title,

                StartDateTime = x.StartDateTime,
                EndDateTime = x.EndDateTime,

            }).ToList();
            return new EventsResponseModel
            {
                Data = eventList,
                Message = "Assets found",
                Success = true,
            };
        }
        public async Task<EventsResponseModel> GetEventsToDisplayAsync(int UserId)
        {
            var eventToDisplay = await _eventRepository.GetEventsToDisplayAsync(UserId);
            if (eventToDisplay.Count == 0)
            {
                return new EventsResponseModel
                {
                    Message = "No events available in your dashboard",
                    Success = false,
                };
            }
            return new EventsResponseModel
            {
                Data = eventToDisplay.Select(x => new EventDto()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Location = x.Location,
                    StartDateTime = x.StartDateTime,
                    EndDateTime = x.EndDateTime,


                }).ToList(),
            };

        }
        public async Task<EventsResponseModel> GetEventsByDateAsync(DateTime date)
        {
            var @event = await _eventRepository.GetEventsByDate(date);
            if (@event == null)
            {
                return new EventsResponseModel
                {
                    Message = $"No events found for {date}",
                    Success = false,
                };
            }
            return new EventsResponseModel
            {
                Data = @event.Select(x => new EventDto()
                {
                    Title = x.Title,
                    Id = x.Id,
                    StartDateTime = x.StartDateTime,
                }).ToList(),
            };
        }
        public async Task<EventResponseModel> GetEventByIdAsync(int id)
        {
            var _event = await _eventRepository.GetAsync(id);
            if (_event == null)
            {
                return new EventResponseModel
                {
                    Message = "Events not found",
                    Success = false,
                };
            }

            return new EventResponseModel
            {
                Message = "Event retrieved successfully",
                Success = true,
                Data = new EventDto
                {
                    Title = _event.Title,
                    Id = _event.Id,
                    StartDateTime = _event.StartDateTime,
                    Location = _event.Location,
                }
            };
        }
    }


}


