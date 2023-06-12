using P013KatmanliBlog.Data;
using P013KatmanliBlog.Service.Abstract;
using P013KatmanliBlog.Service.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies; // Oturum i�lemleri i�in manuel eklemek gerekir...

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession(); // Uygulamada Sesion kullanarak, kimlik ta��y�c�l��� yapmak i�in...



builder.Services.AddDbContext<DatabaseContext>();

builder.Services.AddTransient(typeof(IService<>), typeof(Service<>)); // Kendi yazd���m�z Db i�lemlerini yapan servisi .net core da bu �ekilde MVC projesine servis olarak tan�t�yoruz ki kullanabilelim.

builder.Services.AddTransient<IPostService, PostService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x=>
{
    x.LoginPath = "/Admin/Login/Index"; // oturum a�mayan kullan�c�lar�n giri� i�in g�nderilece�i adres
    x.LogoutPath = "/Admin/Logout";
    x.AccessDeniedPath = "/AccessDenied"; // yetkilendirme ile ekrana eri�im hakk� olmayan kullan�c�lar�n g�nderilece�i sayfa
    x.Cookie.Name = "Administrator"; // Cookie ismi
    x.Cookie.MaxAge = TimeSpan.FromHours(2); // Olu�acak cookie'nin ya�am s�resi... 2 saat
});

// Uygulama admin paneli i�in yetkilendirme ayarlar�

builder.Services.AddAuthorization(x=>
{
    x.AddPolicy("AdminPolicy", p=> p.RequireClaim("Role", "Admin")); // Admin paneline giri� yapma yetkisi ve protokollerini belirttik.
    x.AddPolicy("UserPolicy", p=> p.RequireClaim("Role","User")); // User yetkileri i�in, �rne�in standart userlere verilecek yetkiler i�in kullan�l�r...
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

app.UseSession(); // Session kullanabilmek i�in...

app.UseAuthentication(); // Oturum a�ma i�lemi, user login i�lemlerine gelir.

app.UseAuthorization(); // Yetkilendirme i�lemlerini sorgular.

app.MapControllerRoute(
            name: "admin",
            pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
