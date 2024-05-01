namespace ReminderApplication.DTOs.ResponseModels
{
    public class AdminResponseModel : BaseResponse
    {
        public AdminDto Data { get; set; }
    }
    public class AdminsResponseModel : BaseResponse
    {
        public List<AdminDto> Data { get; set; } = new List<AdminDto>();
    }
}
