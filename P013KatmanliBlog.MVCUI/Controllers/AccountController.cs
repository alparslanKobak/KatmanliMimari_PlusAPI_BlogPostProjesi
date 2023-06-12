using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.MVCUI.Models;
using P013KatmanliBlog.MVCUI.Utils;
using P013KatmanliBlog.Service.Abstract;

namespace P013KatmanliBlog.MVCUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IService<User> _service;

        public AccountController(IService<User> service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("userId");

            if (userId == null)
            {
                TempData["Message"] = "<div class='alert alert-danger'> Lütfen Giriş Yapınız! </div>";

                return RedirectToAction(nameof(Login));
            }
            else
            {
                User us = await _service.GetAsync(a => a.Id == userId);

                return View(us);
            }

        }
        [HttpPost]
        public async Task<IActionResult> UpdateUser(User? user, IFormFile? ProfilePicture)
        {
            if (user == null)
            {
                return RedirectToAction(nameof(Login));
            }

            try
            {
                var userId = HttpContext.Session.GetInt32("userId"); // Id'yi backend kısmında saklayarak işlemlerimizi bu kısımda gerçekleştiriyoruz. Session üzerinden veri çekiyoruz.

                User us = await _service.GetAsync(a=> a.Id == userId);

                if (us != null)
                {
                    // Bir profil resmi gönderilmişse, profil resmi güncellensin.
                    if (ProfilePicture is not null)
                    {
                        us.ProfilePicture = await FileHelper.FileLoaderAsync(ProfilePicture);
                    }
                    
                    us.Name = user.Name;
                    us.UserName = user.UserName;
                    us.Surname = user.Surname;
                    us.Email = user.Email;
                    us.Password = user.Password;
                    us.Phone = user.Phone;

                    if (ModelState.IsValid)
                    {
                        _service.Update(us);
                        await _service.SaveAsync();

                        TempData["Message"] = "<div class='alert alert-success' >Hesap Başarıyla Güncellendi... </div>";

                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Message"] = "<div class='alert alert-danger' > Bir Hata Oluştu... </div>";

                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (Exception e)
            {

                ModelState.AddModelError("","Güncelleme Başarısız" + e.Message);
            }

            return View(nameof(Index),user);
        }

        public IActionResult Login()
        {


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel loginModel)
        {

            User us = await _service.GetAsync(x => x.Email == loginModel.Email && x.Password == loginModel.Password && x.IsActive);

            if (us == null)
            {
                ModelState.AddModelError("", "Giriş Başarısız");
            }
            else
            {
                HttpContext.Session.SetInt32("userId", us.Id);
                HttpContext.Session.SetString("userGuid", us.UserGuid.ToString());

                return RedirectToAction(nameof(Index));
            }

            return View();
        }


        public IActionResult SignIn()
        {


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(User? user, IFormFile? ProfilePicture)
        {

            if (user == null)
            {
                return BadRequest();

            }

            try
            {
                if (ModelState.IsValid)
                {
                    User us = await _service.GetAsync(x=> x.Email == user.Email);

                    if (us != null)
                    {
                        ModelState.AddModelError("","Bu mail ile kayıt daha önce alınmıştır!");
                        return View();
                    }
                    else
                    {
                        if (ProfilePicture is not null)
                        {
                            user.ProfilePicture = await FileHelper.FileLoaderAsync(ProfilePicture);
                        }

                        user.UserGuid = Guid.NewGuid();
                        user.IsActive = true;
                        user.IsAdmin = false;
                        await _service.AddAsync(user);
                        await _service.SaveAsync();

                        TempData["Message"] = "<div class='alert alert-success' > Kayıt Başarıyla Oluştu...</div>";

                        return RedirectToAction(nameof(Login),"Account");
                    }
                }

                
            }
            catch (Exception e)
            {

                ModelState.AddModelError("","Hata Oluştu" + e.Message);
            }

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

            return RedirectToAction("Index","Home"); 
            // Bu şekilde de bir yazım olur mu kontrol edildi...

            
        }


        public IActionResult NewPassword()
        {
            return View();
        }


    }
}
