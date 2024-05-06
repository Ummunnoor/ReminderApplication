using Microsoft.AspNetCore.Mvc;
using ReminderApplication.DTOs.RequestModels;
using ReminderApplication.Entities.Enums;
using ReminderApplication.Interfaces.Services;

namespace ReminderApplication.Controllers
{
    public class ReminderController : Controller
    {
        private readonly IReminderService _reminderService;
        private readonly IEventService _eventService;
        public ReminderController(IReminderService reminderService, IEventService eventService)
        {
            _reminderService = reminderService;
            _eventService = eventService;
        }
        public async Task<IActionResult> Create(CreateReminderRequestModel model)
        {
            if (HttpContext.Request.Method == "POST")
            {
                var reminders = await _reminderService.CreateReminderAsync(model);
                if (reminders.Success == true)
                {
                    return Content(reminders.Message);
                }
                return Content(reminders.Message);

            }
            return View(model);
        }
        public async Task<IActionResult> Update(UpdateReminderRequestModel model)
        {
            if (HttpContext.Request.Method == "POST")
            {
                var reminders = await _reminderService.UpdateReminderAsync(model);
                if (reminders.Success == true)
                {
                    return Content(reminders.Message);
                }
                return Content(reminders.Message);
            }
            return View(model);
        }
        public async Task<IActionResult> Terminate(int EventId,EventStatus Status)
        {
            if (HttpContext.Request.Method == "POST")
            {
                var reminders = await _reminderService.TerminateReminderAsync(EventId,Status);
                if (reminders.Success == true)
                {
                    return Content(reminders.Message);
                }
                return Content(reminders.Message);
            }
            return View();
        }
        public async Task<IActionResult> Get(int EventId)
        {
            var reminders = await _reminderService.GetRemindersByEventIdAsync(EventId);
            return View(reminders);
        }
        public async Task<IActionResult> DisplayReminder()
        {
            var reminders = await _reminderService.GetRemindersToDisplayAsync();
            return View(reminders);
        }
    }
}

