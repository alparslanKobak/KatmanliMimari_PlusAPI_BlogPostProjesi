using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;

namespace P013KatmanliBlog.WebAPIUsing.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "AdminPolicy")]
    public class CategoriesController : Controller
    {
        private readonly HttpClient _httpClient;

        private readonly string _apiAdres = "https://localhost:7223/api/Categories";
        public CategoriesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: CategoriesController
        public async Task<ActionResult> Index()
        {
            var model = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdres);
            return View(model);
        }

        // GET: CategoriesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoriesController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Category collection)
        {
            try
            {
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

        // GET: CategoriesController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Category>(_apiAdres + "/" + id);
            return View(model);
        }

        // POST: CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Category collection)
        {
            try
            {
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

        // GET: CategoriesController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var data = await _httpClient.GetFromJsonAsync<Category>(_apiAdres + "/" + id);

            return View(data);
        }

        // POST: CategoriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Category collection)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(_apiAdres + "/" + id);

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
    }
}
