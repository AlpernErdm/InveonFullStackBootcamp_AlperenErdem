using LibraryWebApplication.Context;
using LibraryWebApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "My API",
        Version = "v1",
        Description = "ASP.NET Core Web API"
    });
});

builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    //Bu detaylarla password gereksinimlerini de�i�tirebiliriz.
    /*  options.Password.RequireNonAlphanumeric=false;
      options.Password.RequireDigit=false;
      options.Password.RequireLowercase=false;
      options.Password.RequireUppercase=false;
      options.Password.RequiredLength = 1;
    */
    //options.User.RequireUniqueEmail = true; //bu �zellikle email unique yapm� oluruz.
    // options.SignIn.RequireConfirmedEmail = true; //bu �zellikle sadece onaylanm�� email ile giri� yapabilirsin

    options.SignIn.RequireConfirmedEmail = true;
    options.Lockout.MaxFailedAccessAttempts = 3; //3 ba�ar�s�z giri�ten sonra hesab� kitler
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);//30 sn kitler


}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();   
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
// specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty; // Swagger ana sayfas�n�n k�k dizinde bar�nmas� i�in (opsiyonel)
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
