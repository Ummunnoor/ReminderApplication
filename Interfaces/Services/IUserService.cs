using ReminderApplication.DTOs.RequestModels;
using ReminderApplication.DTOs.ResponseModels;

namespace ReminderApplication.Interfaces.Services
{
    public interface IUserService
    {
        Task<BaseResponse> Register(CreateUserRequestModel model);
        Task<UserResponseModel> Login(string email, string passWord);
        Task<BaseResponse> UpdateUser(UpdateUserRequestModel updatedUser);

        Task<UserResponseModel> GetById(int id);

    }
}
