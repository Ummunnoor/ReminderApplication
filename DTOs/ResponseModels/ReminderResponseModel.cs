namespace ReminderApplication.DTOs.ResponseModels
{
    
        public class ReminderResponseModel : BaseResponse
        {
            public ReminderDto Data { get; set; }
        }
        public class RemindersResponseModel : BaseResponse
        {
            public List<ReminderDto> Data { get; set; } = new List<ReminderDto>();
        }
    }

