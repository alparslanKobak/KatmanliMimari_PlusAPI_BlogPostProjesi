using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;

namespace P013KatmanliBlog.WebAPIUsing.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly HttpClient _httpClient;

        public CategoriesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private readonly string _apiAdres = "https://localhost:7223/api/";
        public async Task<IActionResult> Index(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Category>(_apiAdres + "Categories/" + id);

            if (model == null)
            {
                TempData["Message"] = "<div class='alert alert-danger'> Aradığınız kategori bulunamadı... Anasayfaya Yönlendiriliyorsunuz... </div>";
                return RedirectToAction(nameof(Index),"Home");
            }
            var category = await _httpClient.GetFromJsonAsync<List<Post>>(_apiAdres + "Post");
            model.Posts = category.Where(x => x.CategoryId == id ).ToList();
            return View(model);
        }
    }
}
