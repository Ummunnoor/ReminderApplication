using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ReminderApplication.DTOs.RequestModels;
using ReminderApplication.Interfaces.Services;
using System.Security.Claims;

namespace ReminderApplication.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Register(CreateUserRequestModel model)
        {
            if (HttpContext.Request.Method == "POST")
            {
                var user = await _userService.Register(model);
                if (user.Success == true)
                {
                    return Content(user.Message);
                }
                return Content(user.Message);
            }
            return View();
        }

        public async Task<IActionResult> GetUser(int Id)
        {
            if (HttpContext.Request.Method == "GET")
            {
                var user = await _userService.GetById(Id);
                if (user.Success == false)
                {
                    return Content(user.Message);
                }
                return Content(user.Message);

            }
            return View();
        }

        public async Task<IActionResult> Update(UpdateUserRequestModel model)
        {
            if (HttpContext.Request.Method == "POST")
            {
                var user = await _userService.UpdateUser(model);
                if (user.Success == true)
                {
                    return Content(user.Message);
                }
                return Content(user.Message);
            }
            return View();
        }
        public async Task<IActionResult> Login(string email, string password)
        
        {
            if (HttpContext.Request.Method == "POST")
            {
                var login = await _userService.Login(email, password);
                if (login.Success == false)
                {
                    return Content("Email or Password does not exist ");
                }

                var claims = new List<Claim>
                {
                    new Claim (ClaimTypes.NameIdentifier, (login.Data.Id).ToString()),
                    new Claim (ClaimTypes.Email, login.Data.Email),

                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authenticationProperties = new AuthenticationProperties();
                var principal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);
                return RedirectToRoute(new { controller = "User", action = "GetUser", id = $"{login.Data.Id}" });
            }
            return View();

        }


        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }



    }
}

