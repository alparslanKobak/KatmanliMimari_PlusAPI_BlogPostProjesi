using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.WebAPIUsing.Models;
using P013KatmanliBlog.WebAPIUsing.Utils;

namespace P013KatmanliBlog.WebAPIUsing.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;

        public AccountController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        private readonly string _apiAdres = "https://localhost:7223/api/User";
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("userId");

            if (userId == null)
            {
                TempData["Message"] = "<div class='alert alert-danger'>Lütfen Giriş Yapınız!</div>";

                return RedirectToAction(nameof(Login), "Account");
            }
            else
            {
                var user = await _httpClient.GetFromJsonAsync<User>(_apiAdres + "/" + userId);

                return View(user);
            }
        }

        public IActionResult SignIn()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(User user, IFormFile? ProfilePicture)
        {
            var users = await _httpClient.GetFromJsonAsync<List<User>>(_apiAdres);

            try
            {
                if (ModelState.IsValid)
                {
                    var kullanici = users.FirstOrDefault(x=> x.Email == user.Email);

                    if (kullanici != null)
                    {
                        TempData["Message"] = "<div class='alert alert-danger'>Bu mail ile kayıt zaten mevcut!</div>";

                        return View(user);
                    }
                    else
                    {
                        if (ProfilePicture != null)
                        {
                            user.ProfilePicture = await FileHelper.FileLoaderAsync(ProfilePicture);
                        }
                        user.UserGuid = Guid.NewGuid();
                        user.IsActive = true;
                        user.IsAdmin = false;

                        var sonuc = await _httpClient.PostAsJsonAsync(_apiAdres, user);

                        if (sonuc.IsSuccessStatusCode)
                        {
                            TempData["Message"] = "<div class='alert alert-success'>Başarı ile kayıt oluşturuldu...</div>";

                            return RedirectToAction(nameof(Login), "Account");
                        }
                    }
                }
            }
            catch (Exception e)
            {

                ModelState.AddModelError("", "Hata oluştu" + e.Message);
            }
            return View(user);
        }

        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel userLoginModel)
        {
            var users = await _httpClient.GetFromJsonAsync<List<User>>(_apiAdres);

            var user = users.FirstOrDefault(x=> x.Email == userLoginModel.Email && x.Password == userLoginModel.Password && x.IsActive);

            if (user == null)
            {
                TempData["Message"] = "<div class='alert alert-danger'>Giriş Başarısız!</div>";
            }
            else
            {
                TempData["Message"] = "<div class='alert alert-success'>Giriş Başarılı!</div>";
                HttpContext.Session.SetInt32("userId",user.Id);
                HttpContext.Session.SetString("userGuid",user.UserGuid.ToString());

                return RedirectToAction(nameof(Index),"Home");
            }

            return View();
        }

        public IActionResult NewPassword()
        {
            return View();
        }
        
        public IActionResult Logout()
        {
            try
            {
                HttpContext.Session.Remove("userId");
                HttpContext.Session.Remove("userGuid");
            }
            catch 
            {

                HttpContext.Session.Clear();
            }
            return RedirectToAction(nameof(Index),"Home");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(User? user, IFormFile? ProfilePicture)
        {
            if (user == null)
            {
                return NotFound();
            }

            try
            {

                if (ProfilePicture != null)
                {
                    user.ProfilePicture = await FileHelper.FileLoaderAsync(ProfilePicture);
                }

                var userId = HttpContext.Session.GetInt32("userId");

                var kullanici = await _httpClient.GetFromJsonAsync<User>(_apiAdres + "/" + userId);

                if (kullanici != null)
                {
                    
                    kullanici.Name = user.Name;

                    kullanici.Surname = user.Surname;

                    kullanici.ProfilePicture = user.ProfilePicture;

                    kullanici.Email = user.Email;

                    kullanici.Phone = user.Phone;

                    if (ModelState.IsValid)
                    {
                        var response = await _httpClient.PutAsJsonAsync(_apiAdres, kullanici);

                        if (response.IsSuccessStatusCode)
                        {
                            TempData["Message"] = "<div class='alert alert-success'>Güncelleme Başarılı!</div>";

                            return RedirectToAction(nameof(Index),kullanici);
                        }
                    }
                }
            }
            catch (Exception e)
            {

                ModelState.AddModelError("", "Güncelleme Başarısız" + e.Message);
            }

            return View(nameof(Index),user);
        }
    }
}
