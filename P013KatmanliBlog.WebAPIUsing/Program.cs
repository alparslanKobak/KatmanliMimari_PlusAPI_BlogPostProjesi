using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession();

builder.Services.AddHttpClient();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x=>
{
    x.LoginPath = "/Admin/Login";
    x.LogoutPath = "/Admin/Logout";
    x.AccessDeniedPath = "/AccessDenied";
    x.Cookie.Name = "Administrator";
    x.Cookie.MaxAge = TimeSpan.FromHours(2);
});


builder.Services.AddAuthorization(x=>
{
    x.AddPolicy("AdminPolicy", p=> p.RequireClaim("Role","Admin"));
    x.AddPolicy("UserPolicy", p=> p.RequireClaim("Role","User"));
});
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

app.UseSession();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
           name: "admin",
           pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
         );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
