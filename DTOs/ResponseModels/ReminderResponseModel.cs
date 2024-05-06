namespace ReminderApplication.DTOs.ResponseModels
{
    
        public class ReminderResponseModel : BaseResponse<ReminderDto>
        {
            public ReminderDto Data { get; set; }
        }
        public class RemindersResponseModel : BaseResponse<List<ReminderDto>>
        {
            public List<ReminderDto> Data { get; set; } = new List<ReminderDto>();
        }
    }

