using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ReminderApplication.Context;
using ReminderApplication.EmailServices;
using ReminderApplication.Implementations.Repositories;
using ReminderApplication.Implementations.Services;
using ReminderApplication.Interfaces.Repositories;
using ReminderApplication.Interfaces.Services;
using ReminderApplication.SmsServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("ReminderConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("ReminderConnection"))));
builder.Services.AddAuthentication
    (CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(config =>
    {
        config.LoginPath = "/ReminderApplication/login";
        config.Cookie.Name = "ReminderApplication";
        config.LogoutPath = "/ReminderApplication/Logout";
    });
builder.Services.AddAuthentication();

builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IEventRepository, EventRepository>();

builder.Services.AddScoped<IReminderService, ReminderService>();
builder.Services.AddScoped<IReminderRepository, ReminderRepository>();

builder.Services.AddScoped<IMailServices, MailService>();

builder.Services.AddScoped<ISmsService, SmsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
