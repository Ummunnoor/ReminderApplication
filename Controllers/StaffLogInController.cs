using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ReminderApplication.Context;
using ReminderApplication.Interfaces.Services;
using System.Security.Claims;

namespace ReminderApplication.Controllers
{
    public class StaffLogInController : Controller
    {
        private readonly ApplicationContext dbContext;

        private readonly IUserService _userService;

        public StaffLogInController(IUserService userService)
        {

            _userService = userService;
        }
        public async Task<IActionResult> LogInStaff(string email, string password)
        {
            if (HttpContext.Request.Method == "POST")
            {
                var login = await _userService.Login(email, password);

                if (login == null)
                {
                    return Content("Email or Password does not exist ");
                }

                var claims = new List<Claim>
                {

                    new Claim (ClaimTypes.NameIdentifier, login.Data.Email),
                    new Claim (ClaimTypes.NameIdentifier, login.Data.Password)

                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authenticationProperties = new AuthenticationProperties();
                var principal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);
                return RedirectToAction("Index", "Admin");
            }
            return View();

        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("StaffLogin");
        }



    }
}
