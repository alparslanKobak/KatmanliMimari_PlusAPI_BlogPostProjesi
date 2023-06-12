using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.WebAPIUsing.Utils;

namespace P013KatmanliBlog.WebAPIUsing.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "AdminPolicy")]
    public class UsersController : Controller
    {

        private readonly HttpClient _httpClient;

        public UsersController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private readonly string _apiAdres = "https://localhost:7223/api/User";
        // GET: UsersController
        public async Task<ActionResult> Index()
        {
            var users = await _httpClient.GetFromJsonAsync<List<User>>(_apiAdres);
            return View(users);
        }

        // GET: UsersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(User collection, IFormFile? ProfilePicture)
        {
            try
            {
                if (ProfilePicture is not null)
                {
                    collection.ProfilePicture = await FileHelper.FileLoaderAsync(ProfilePicture);
                }

                var response = await _httpClient.PostAsJsonAsync(_apiAdres, collection);


                if (response.IsSuccessStatusCode)
                {

                    return RedirectToAction(nameof(Index));
                }

            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Hata Oluştu : " + e.Message);
            }
            return View(collection);
        }

        // GET: UsersController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<User>(_apiAdres + "/" + id);
            return View(model);
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, User collection, IFormFile? ProfilePicture)
        {
            try
            {
                if (ProfilePicture is not null)
                {
                    collection.ProfilePicture = await FileHelper.FileLoaderAsync(ProfilePicture);
                }

                var response = await _httpClient.PutAsJsonAsync(_apiAdres, collection);


                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));

                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Hata Oluştu : " + e.Message);
            }
            return View(collection);
        }

        // GET: UsersController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<User>(_apiAdres + "/" + id);
            return View(model);
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, User collection)
        {
            try
            {
                await _httpClient.DeleteAsync(_apiAdres + "/" + id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
