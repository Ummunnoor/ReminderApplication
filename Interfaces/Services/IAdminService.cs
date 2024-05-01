using ReminderApplication.DTOs.RequestModels;
using ReminderApplication.DTOs.ResponseModels;

namespace ReminderApplication.Interfaces.Services
{
    public interface IAdminService
    {
        Task<BaseResponse> AddAdminAsync(CreateAdminRequestModel model);

        Task<BaseResponse> DeleteAdmin(int Id);
        Task<AdminResponseModel> GetById(int Id);
        Task<AdminsResponseModel> GetAllAdmin();

    }
}
