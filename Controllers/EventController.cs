using Microsoft.AspNetCore.Mvc;
using ReminderApplication.DTOs.RequestModels;
using ReminderApplication.Interfaces.Services;
using System.Security.Claims;

namespace ReminderApplication.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IUserService _userService;


        public EventController(IEventService eventService, IUserService userService)
        {
            _eventService = eventService;
            _userService = userService;
        }
        public async Task<IActionResult> CreateEvent(CreateEventRequestModel model)
        {
            if (HttpContext.Request.Method == "POST")
            {
                var context = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
                var @event = await _eventService.CreateEventAsync(context, model);
                if (@event.Success == true)
                {
                    return Content(@event.Message);
                }
                return Content(@event.Message);
            }
            return View();
        }
        public async Task<IActionResult> UpdateEvent(UpdateEventRequestModel model)
        {
            if (HttpContext.Request.Method == "POST")
            {
                var @event = await _eventService.UpdateEventAsync(model);
                if (@event.Success == true)
                {
                    return Content(@event.Message);
                }
                return Content(@event.Message);
            }
            return View();
        }
        public async Task<IActionResult> CancelEvent(int id)
        {
            if (HttpContext.Request.Method == "POST")
            {
                var @event = await _eventService.CancelEventAsync(id);
                {
                    if (@event.Success == true)
                    {
                        return Content(@event.Message);
                    }
                    return Content(@event.Message);
                }
            }
            return View();
        }
        public async Task<IActionResult> GetEvents()
        {
            var events = await _eventService.GetAllEventsAsync();
            return View(events);
        }
        public async Task<IActionResult> DisplayEvents(int UserId)
        {
            var events = await _eventService.GetEventsToDisplayAsync(UserId);
            return View(events);
        }
        public async Task<IActionResult> GetEventsByDate(DateTime date)
        {
            var events = await _eventService.GetEventsByDateAsync(date);
            return View(events);
        }
        public async Task<IActionResult> Get(int id)
        {
            var events = await _eventService.GetEventByIdAsync(id);
            return View(events);
        }

    }

}

