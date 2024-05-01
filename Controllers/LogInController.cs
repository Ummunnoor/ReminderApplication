using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ReminderApplication.Context;
using ReminderApplication.DTOs.RequestModels;
using ReminderApplication.Interfaces.Services;
using System.Security.Claims;

namespace ReminderApplication.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationContext dbContext;
        private readonly IUserService _userService;
        private readonly IAdminService _adminService;


        public LoginController(IUserService userService, IAdminService adminService)
        {
            _userService = userService;

            _adminService = adminService;
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
                else if (login.Success == true)
                {
                    return RedirectToRoute(new { controller = "User", action = "Index", id = $"{login.Data.Id}" });
                }
                else if (login.Success == true)
                {
                    var claims = new List<Claim>
                {
                    new Claim (ClaimTypes.NameIdentifier, (login.Data.Id).ToString()),
                    new Claim (ClaimTypes.NameIdentifier, login.Data.Email),
                    new Claim (ClaimTypes.NameIdentifier, login.Data.Password),
                };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authenticationProperties = new AuthenticationProperties();
                    var principal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);
                    var admin = await _adminService.GetById(login.Data.Id);

                    if (admin.Data.UserName == "Admin")
                    {
                        return RedirectToRoute(new { controller = "Admin", action = "Index", id = $"{login.Data.Id}" });
                    }
                }


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

