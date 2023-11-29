using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Technocitt.Data;
using Technocitt.Services;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;
System.Net.ServicePointManager.ServerCertificateValidationCallback +=
    (sender, cert, chain, sslPolicyErrors) => true;

builder.Services.AddDbContextPool<AppDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("LiveServer")));

builder.Services.AddScoped<IUsersProvider, UsersProvider>();
builder.Services.AddScoped<IPropertiesProvider, PropertiesProvider>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddAuthentication(options =>
{
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.ExpireTimeSpan = TimeSpan.FromHours(24);
    options.AccessDeniedPath = "/Account/Login";
});
builder.Services.AddControllersWithViews();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

 app.MapControllers();

app.MapControllerRoute(
       name: "default",
          pattern: "{controller=Home}/{action=Index}");
app.UseAuthorization();
app.UseStaticFiles();

app.Run();
