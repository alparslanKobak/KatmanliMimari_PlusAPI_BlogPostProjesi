using P013KatmanliBlog.Data;
using P013KatmanliBlog.Service.Abstract;
using P013KatmanliBlog.Service.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies; // Oturum iþlemleri için manuel eklemek gerekir...

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession(); // Uygulamada Sesion kullanarak, kimlik taþýyýcýlýðý yapmak için...



builder.Services.AddDbContext<DatabaseContext>();

builder.Services.AddTransient(typeof(IService<>), typeof(Service<>)); // Kendi yazdýðýmýz Db iþlemlerini yapan servisi .net core da bu þekilde MVC projesine servis olarak tanýtýyoruz ki kullanabilelim.

builder.Services.AddTransient<IPostService, PostService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x=>
{
    x.LoginPath = "/Admin/Login/Index"; // oturum açmayan kullanýcýlarýn giriþ için gönderileceði adres
    x.LogoutPath = "/Admin/Logout";
    x.AccessDeniedPath = "/AccessDenied"; // yetkilendirme ile ekrana eriþim hakký olmayan kullanýcýlarýn gönderileceði sayfa
    x.Cookie.Name = "Administrator"; // Cookie ismi
    x.Cookie.MaxAge = TimeSpan.FromHours(2); // Oluþacak cookie'nin yaþam süresi... 2 saat
});

// Uygulama admin paneli için yetkilendirme ayarlarý

builder.Services.AddAuthorization(x=>
{
    x.AddPolicy("AdminPolicy", p=> p.RequireClaim("Role", "Admin")); // Admin paneline giriþ yapma yetkisi ve protokollerini belirttik.
    x.AddPolicy("UserPolicy", p=> p.RequireClaim("Role","User")); // User yetkileri için, örneðin standart userlere verilecek yetkiler için kullanýlýr...
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

app.UseSession(); // Session kullanabilmek için...

app.UseAuthentication(); // Oturum açma iþlemi, user login iþlemlerine gelir.

app.UseAuthorization(); // Yetkilendirme iþlemlerini sorgular.

app.MapControllerRoute(
            name: "admin",
            pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
