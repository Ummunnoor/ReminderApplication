using ReminderApplication.DTOs.RequestModels;
using ReminderApplication.DTOs.ResponseModels;
using ReminderApplication.DTOs;
using ReminderApplication.Entities;
using ReminderApplication.Implementations.Repositories;
using ReminderApplication.Interfaces.Services;
using ReminderApplication.Interfaces.Repositories;

namespace ReminderApplication.Implementations.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        public EventService(IEventRepository eventrepository)
        {
            _eventRepository = eventrepository;
        }
        public async Task<BaseResponse> CreateEventAsync(int UserId, CreateEventRequestModel model)
        {
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

            };

            await _eventRepository.CreateAsync(@event);
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


