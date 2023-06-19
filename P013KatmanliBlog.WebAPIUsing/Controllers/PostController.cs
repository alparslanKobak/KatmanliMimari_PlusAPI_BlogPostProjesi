using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.WebAPIUsing.Models;

namespace P013KatmanliBlog.WebAPIUsing.Controllers
{
    public class PostController : Controller
    {
        private readonly HttpClient _httpClient;

        public PostController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private readonly string _apiAdres = "https://localhost:7223/api/Post";
        public async Task<IActionResult> Index()
        {
            var model = await _httpClient.GetFromJsonAsync<List<Post>>(_apiAdres);

            return View(model);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                ModelState.AddModelError("","Hata Oluştu");

                return RedirectToAction(nameof(Index),"Home");
            }

            PostDetailViewModel model = new();

            var posts = await _httpClient.GetFromJsonAsync<List<Post>>(_apiAdres);

            var post = await _httpClient.GetFromJsonAsync<Post>(_apiAdres + "/" + id);

            if (post == null)
            {
                TempData["Message"] = "<div class='alert alert-danger'>Beklenmedik bir hata oluştu!</div>";
                return RedirectToAction(nameof(Index), "Home");
            }

            model.Post = post;

            if (posts == null)
            {
                TempData["Message"] = "<div class='alert alert-success'>Bu kadarcıkmış ...</div>";
            }
            model.RelatedPosts = posts.Where(x=> x.CategoryId == post.CategoryId && x.Id!= id).ToList();

            if (model == null)
            {
                ModelState.AddModelError("", "Hata Oluştu");

                return RedirectToAction(nameof(Index), "Home");
            }

            return View(model);

           
        }
    }
}
