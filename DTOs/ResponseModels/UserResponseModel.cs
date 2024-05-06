namespace ReminderApplication.DTOs.ResponseModels
{
    public class UserResponseModel : BaseResponse<UserDto>
    {
        public UserDto Data { get; set; }
    }
    public class UsersResponseModel : BaseResponse<List<UserDto>>
    {
        public List<UserDto> Data { get; set; } = new List<UserDto>();
    }
}
