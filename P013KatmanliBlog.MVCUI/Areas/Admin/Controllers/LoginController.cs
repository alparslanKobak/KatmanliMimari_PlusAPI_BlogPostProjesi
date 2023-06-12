using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.MVCUI.Models;
using P013KatmanliBlog.Service.Abstract;
using System.Security.Claims;

namespace P013KatmanliBlog.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly IService<User> _service;

        public LoginController(IService<User> service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost] // Login ekranının ön yüze ulaşması adına önce Get metodlu Index'i çağırdık. Ardından Post ile validation input'umuzu yollayacağız.
        public async Task<IActionResult> Index(AdminLoginModel admin)
        {

            try
            {
                User us = await _service.GetAsync(x=> x.IsActive && x.Email == admin.Email && x.Password == admin.Password);

                if (us != null)
                {
                    List<Claim> userYetkileri = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, us.Email),
                        new Claim("Role",us.IsAdmin? "Admin" : "User"),
                        new Claim("UserGuid", us.UserGuid.ToString()) // Benzersiz kimlik
                    };

                    ClaimsIdentity usId = new ClaimsIdentity(userYetkileri,"Login"); // Login tipinde bir authentication olayı yarattık.

                    ClaimsPrincipal principal = new(usId); // Bir kimliği oluşturarak, işlevini belirledik. Örneğin TC kimlik kartı yahut pasaport... Pasaportun işlevi uluslararası ortamda kimliği temsil eder, ona göre prop alanları belirlenmiştir. Bunun işlevi de KatmanliBlog sitesinde temsil edecek değerlere ve prensiplere sahip olmak...

                    await HttpContext.SignInAsync(principal); // HttpContext protokolü üzerinden Login işlemini .Net kütüphanesinden çağırdık.

                    return RedirectToAction("Index","Main");

                }
                else
                {
                    ModelState.AddModelError("", "Giriş Başarısız ! ");
                }
            }
            catch (Exception e)
            {

                ModelState.AddModelError("", "Hata Oluştu : " + e.Message);
            }
            return View();
        }

        [Route("Logout")] // Çıkış işlemi
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(); // Sistemdeki session'u at. Çıkış yap...

            return Redirect("/Admin/Login");
        }
    }
}
