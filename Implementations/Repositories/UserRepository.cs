using Microsoft.EntityFrameworkCore;
using ReminderApplication.Context;
using ReminderApplication.DTOs.ResponseModels;
using ReminderApplication.Entities;
using ReminderApplication.Interfaces.Repositories;
using static ReminderApplication.Implementations.Repositories.UserRepository;

namespace ReminderApplication.Implementations.Repositories
{
    
    
        public class UserRepository : GenericRepository<User>, IUserRepository
        {
            public UserRepository(ApplicationContext Context)
            {
                _Context = Context;
            }
            public async Task<BaseResponse> ExistsByEmailAsync(string Email, string passWord)
            {
                var user = await _Context.Users
                .FirstOrDefaultAsync(c => c.Email == Email && c.Password == passWord);
                if (user == null)
                {
                    return new BaseResponse()
                    {
                        Message = "User Not Found",
                        Success = false,
                    };
                }
                return new BaseResponse()
                {
                    Message = "User Found",
                    Success = true,
                };
            }

            public async Task<User> GetUser(int id)
            {
                var user = await _Context.Users
                .SingleOrDefaultAsync(c => c.Id == id);
                return user;
            }
        }
    }

