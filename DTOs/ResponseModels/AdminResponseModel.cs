namespace ReminderApplication.DTOs.ResponseModels
{
    public class AdminResponseModel : BaseResponse<AdminDto>
    {
        public AdminDto Data { get; set; }
    }
    public class AdminsResponseModel : BaseResponse<List<AdminDto>>
    {
        public List<AdminDto> Data { get; set; } = new List<AdminDto>();
    }
}
