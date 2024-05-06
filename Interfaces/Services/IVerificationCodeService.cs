using ReminderApplication.DTOs;
using ReminderApplication.DTOs.ResponseModels;

namespace ReminderApplication.Interfaces.Services
{
    public interface IVerificationCodeService
    {
        Task<BaseResponse<UserDto>> UpdateVeryficationCodeAsync(int id);
        Task<BaseResponse<UserDto>> VerifyCode(int id, int verificationcode);
    }
}
