using ReminderApplication.DTOs.RequestModels;
using ReminderApplication.DTOs.ResponseModels;
using ReminderApplication.DTOs;
using ReminderApplication.Entities;
using ReminderApplication.Implementations.Repositories;
using ReminderApplication.Interfaces.Services;
using ReminderApplication.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using ReminderApplication.EmailServices;

namespace ReminderApplication.Implementations.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IMailServices _mailServices;

        public AdminService(IUserRepository userRepository, IAdminRepository adminRepository,IMailServices mailServices)
        {
            _userRepository = userRepository;
            _adminRepository = adminRepository;
            _mailServices = mailServices;

        }

        public async Task<BaseResponse> AddAdminAsync(CreateAdminRequestModel model)
        {

            var admin = await _adminRepository.GetAsync(a => a.User.Email == model.Email && a.User.Password == model.Password);
            if (admin != null)
            {
                return new BaseResponse()
                {
                    Message = "Admin Already Exist",
                    Success = false,
                };
            }
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.Username,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                Password = model.Password,
            };
            var adduser = await _userRepository.CreateAsync(user);
            var admins = new Admin
            {
                Id = adduser.Id,
                User = adduser,
                IsDeleted = false,
            };
            var addAdmin = await _adminRepository.CreateAsync(admins);
            var mailRequest = new MailRequest
            {
                Subject = "Welcome To Reminder Application",
                ToEmail = addAdmin.User.Email,
                ToName = addAdmin.User.Email,
                HtmlContent = $"<html><body><h1>Hello {addAdmin.User.Email}, You have successfully created a reminder for {model.FirstName}/'s event.</h1></body></html>",
            };
            _mailServices.SendEMailAsync(mailRequest);
            return new BaseResponse
            {
                Message = "Admin Added Successfully",
                Success = true,
            };

        }

        public async Task<BaseResponse> DeleteAdmin(int Id)
        {
            var admin = await _adminRepository.GetAsync(admins => admins.IsDeleted == false && admins.Id == Id);
            if (admin == null)
            {
                return new BaseResponse
                {
                    Message = "Admin not found",
                    Success = false
                };
            }

            admin.IsDeleted = true;
            await _adminRepository.UpdateAsync(admin);
            return new BaseResponse
            {
                Message = "Administrator Successfully Deleted",
                Success = true
            };
        }
        public async Task<AdminResponseModel> GetById(int Id)
        {
            var admin = await _adminRepository.GetByIdAsync(Id);
            if (admin == null)
            {
                return new AdminResponseModel

                {
                    Message = "Admin not found",
                    Success = false,
                };

            }
            return new AdminResponseModel
            {
                Message = "Admin retrieved successfully",
                Success = true,
                Data = new AdminDto
                {
                    FirstName = admin.User.FirstName,
                    LastName = admin.User.LastName,
                    UserName = admin.User.Username,
                    PhoneNumber = admin.User.PhoneNumber,
                }

            };
        }


        public async Task<AdminsResponseModel> GetAllAdmin()
        {
            var admins = await _adminRepository.GetAllAdminsAsync();
            return new AdminsResponseModel
            {
                Data = admins.Select(x => new AdminDto
                {
                    Id = x.Id,
                    UserName = x.User.Username,
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    PhoneNumber = x.User.PhoneNumber,
                }).ToList()
            };
        }
    }
}
