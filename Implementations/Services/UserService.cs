using ReminderApplication.DTOs.RequestModels;
using ReminderApplication.DTOs.ResponseModels;
using ReminderApplication.DTOs;
using ReminderApplication.Entities;
using ReminderApplication.Implementations.Repositories;
using ReminderApplication.Interfaces.Services;
using ReminderApplication.Interfaces.Repositories;

namespace ReminderApplication.Implementations.Services
{
    public class UserService : IUserService

    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<BaseResponse> Register(CreateUserRequestModel model)
        {
            var user = await _userRepository.GetAsync(u => u.Email == model.Email);
            if (user != null)
            {
                return new BaseResponse()
                {
                    Message = "Email Already Exist",
                    Success = false,
                };
            }
            var _user = new User
            {
                Email = model.Email,
                Password = model.Password,
            };
            var adduser = await _userRepository.CreateAsync(_user);
            var custm = new User()
            {

                Username = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Id = adduser.Id,
            };

            var custom = await _userRepository.CreateAsync(custm);
            if (custom == null)
            {
                return new BaseResponse()
                {
                    Message = "Unable To Register User",
                    Success = false,
                };
            }
            else
            {

                return new BaseResponse()
                {
                    Message = "User Successfully Registered",
                    Success = true,
                };
            }
        }



        public async Task<UserResponseModel> Login(string email, string password)
        {

            var user = await _userRepository.GetAsync(x => x.Email.Equals(email) && x.Password.Equals(password));
            if (user != null)
            {
                return new UserResponseModel
                {
                    Data = new UserDto()
                    {
                        Email = user.Email,
                        Password = user.Password,
                        Id = user.Id
                    },
                    Success = true,
                    Message = "Sucessfully logged in",
                };
            }
            return new UserResponseModel
            {
                Success = false,
                Message = "Loggin Failed",
            };
        }

        public async Task<BaseResponse> UpdateUser(UpdateUserRequestModel updatedUser)
        {
            var user = await _userRepository.GetUser(updatedUser.UserId);
            if (user == null)
            {
                return new BaseResponse
                {
                    Message = "User Not Found",
                    Success = false,
                };
            }
            user.FirstName = updatedUser.FirstName ?? user.FirstName;
            user.LastName = updatedUser.LastName ?? user.LastName;
            user.PhoneNumber = updatedUser.PhoneNumber ?? user.PhoneNumber;
            user.Username = updatedUser.Username ?? user.Username;
            user.Email = updatedUser.Email ?? user.Email;
            await _userRepository.UpdateAsync(user);
            return new BaseResponse
            {
                Message = "User updated successfully",
                Success = true,
            };
        }
        public async Task<UserResponseModel> GetById(int Id)
        {
            var user = await _userRepository.GetUser(Id);
            if (user == null)
            {
                return new UserResponseModel
                {
                    Message = "User not found",
                    Success = false,
                };
            }
            return new UserResponseModel
            {
                Message = "User Retrived Successfully",
                Success = true,
                Data = new UserDto
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                }
            };
        }
    }
}
