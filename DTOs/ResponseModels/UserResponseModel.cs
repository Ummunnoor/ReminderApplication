namespace ReminderApplication.DTOs.ResponseModels
{
    public class UserResponseModel : BaseResponse
    {
        public UserDto Data { get; set; }
    }
    public class UsersResponseModel : BaseResponse
    {
        public List<UserDto> Data { get; set; } = new List<UserDto>();
    }
}
