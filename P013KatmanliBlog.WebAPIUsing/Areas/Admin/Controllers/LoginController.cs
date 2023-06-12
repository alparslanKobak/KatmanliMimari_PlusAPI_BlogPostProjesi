using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.WebAPIUsing.Models;
using System.Security.Claims;

namespace P013KatmanliBlog.WebAPIUsing.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;

        public LoginController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private readonly string _apiAdres = "https://localhost:7223/api/User";

        public IActionResult Index(string ReturnUrl)
        {
            var model = new AdminLoginModel();
            model.ReturnUrl = ReturnUrl;
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Index(AdminLoginModel adminLoginModel)
        {
            try
            {
                var userList = await _httpClient.GetFromJsonAsync<List<User>>(_apiAdres);

                var account = userList.FirstOrDefault(x => x.Email == adminLoginModel.Email && x.Password == adminLoginModel.Password && x.IsActive);

                if (account == null)
                {
                    ModelState.AddModelError("", "Giriş Başarısız!");
                }
                else
                {
                    List<Claim> kullaniciYetkileri = new List<Claim>
                {
                    new Claim(ClaimTypes.Email,account.Email),

                    new Claim("Role",account.IsAdmin?"Admin":"User"),

                    new Claim("UserGuid",account.UserGuid.ToString())
                };

                    ClaimsIdentity userID = new ClaimsIdentity(kullaniciYetkileri, "Login");

                    ClaimsPrincipal principal = new(userID);

                    await HttpContext.SignInAsync(principal); // Protokolü geçtiyse giriş işlemi yapsın.

                    return Redirect(string.IsNullOrEmpty(adminLoginModel.ReturnUrl) ? "/Admin/Main" : adminLoginModel.ReturnUrl);

                }
            }
            catch (Exception e)
            {

                ModelState.AddModelError("", "Hata Oluştu : " + e.Message);
            }

            return View();
        }

        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            
            return Redirect("/Admin/Login");
        }
    }
}
