using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.WebAPIUsing.Utils;

namespace P013KatmanliBlog.WebAPIUsing.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "AdminPolicy")]
    public class PostsController : Controller
    {
        private readonly HttpClient _httpClient;

        public PostsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private readonly string _apiAdres = "https://localhost:7223/api/Post";

        private readonly string _apiAdresKategori = "https://localhost:7223/api/Categories";

        private readonly string _apiAdresUser = "https://localhost:7223/api/User";

        // GET: PostsController
        public async Task<ActionResult> Index()
        {
            var model = await _httpClient.GetFromJsonAsync<List<Post>>(_apiAdres);
            return View(model);
        }

        // GET: PostsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PostsController/Create
        public async Task<ActionResult> Create()
        {
            var kategoriData = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdresKategori);

            ViewBag.CategoryId = new SelectList(kategoriData, "Id", "Name");

            var userData = await _httpClient.GetFromJsonAsync<List<User>>(_apiAdresUser);

            ViewBag.UserId = new SelectList(userData, "Id", "UserName");

            return View();
        }

        // POST: PostsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Post collection, IFormFile? PostImage)
        {
            try
            {
                if (PostImage != null)
                {
                    collection.PostImage = await FileHelper.FileLoaderAsync(PostImage);
                }
                var response = await _httpClient.PostAsJsonAsync(_apiAdres, collection);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));

                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Hata Oluştu" + e.Message);
            }

            var kategoriData = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdresKategori);

            ViewBag.CategoryId = new SelectList(kategoriData, "Id", "Name");

            var userData = await _httpClient.GetFromJsonAsync<List<User>>(_apiAdresUser);

            ViewBag.UserId = new SelectList(userData, "Id", "UserName");

            return View(collection);
        }

        // GET: PostsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var kategoriData = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdresKategori);

            ViewBag.CategoryId = new SelectList(kategoriData, "Id", "Name");

            var userData = await _httpClient.GetFromJsonAsync<List<User>>(_apiAdresUser);

            ViewBag.UserId = new SelectList(userData, "Id", "UserName");

            var model = await _httpClient.GetFromJsonAsync<Post>(_apiAdres + "/" + id);

            return View(model);
        }

        // POST: PostsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Post collection, IFormFile? PostImage)
        {
            try
            {
                if (PostImage != null)
                {
                    collection.PostImage = await FileHelper.FileLoaderAsync(PostImage);
                }

                var response = await _httpClient.PatchAsJsonAsync(_apiAdres, collection);

                if (response.IsSuccessStatusCode)
                {

                    return RedirectToAction(nameof(Index));
                }

            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Hata Oluştu" + e.Message);
            }


            var kategoriData = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdresKategori);

            ViewBag.CategoryId = new SelectList(kategoriData, "Id", "Name");

            var userData = await _httpClient.GetFromJsonAsync<List<User>>(_apiAdresUser);

            ViewBag.UserId = new SelectList(userData, "Id", "UserName");

            var model = await _httpClient.GetFromJsonAsync<Post>(_apiAdres + "/" + id);

            return View(collection);
        }

        // GET: PostsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Post>(_apiAdres + "/" + id);
            return View(model);
        }

        // POST: PostsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Post collection)
        {
            try
            {
                if (collection.PostImage is not null)
                {
                    FileHelper.FileRemover(collection.PostImage);
                }
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
