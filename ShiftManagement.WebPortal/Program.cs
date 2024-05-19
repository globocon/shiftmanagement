using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ShiftManagement.Data;
using ShiftManagement.Data.Providers;
using ShiftManagement.WebPortal.Helpers;
using ShiftManagement.WebPortal.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ShiftDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IUserDataProvider, UserDataProvider>();
builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
builder.Services.AddScoped<IEmployeeDataProvider, EmployeeDataProvider>();
builder.Services.AddScoped<IClientDataProvider, ClientDataProvider>();
builder.Services.AddScoped<IShiftDataProvider, ShiftDataProvider>();
builder.Services.AddScoped<ICompanyDataProvider, CompanyDataProvider>();
builder.Services.AddSession();
builder.Services.AddRazorPages(options =>
{

    options.Conventions.AuthorizeFolder("/");
    options.Conventions.AuthorizeFolder("/Develop");
    options.Conventions.AllowAnonymousToFolder("/Account");
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(120);
});
builder.Services.AddHttpContextAccessor();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

AuthUserHelper.Configure(app.Services.GetService<IHttpContextAccessor>());
UserAuthenticationService.Configure(app.Services.GetService<IHttpContextAccessor>());


app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(name: "clients",
                pattern: "client/{*clientpage}",
                defaults: new { controller = "clients", action = "clientpage" });
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
