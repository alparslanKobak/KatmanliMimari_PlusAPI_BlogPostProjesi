using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.MVCUI.Models;
using P013KatmanliBlog.Service.Abstract;

namespace P013KatmanliBlog.MVCUI.Controllers
{
    public class PostController : Controller
    {
        
        private readonly IPostService _servicePost;

        public PostController(IPostService servicePost)
        {
            _servicePost = servicePost;
        }

        public async Task<IActionResult> Index()
        {

            IEnumerable<Post> model = await _servicePost.GetAllAsync();


            return View(model);
        }

        public async Task<IActionResult> Detail(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            PostDetailViewModel model = new();

            Post post = await _servicePost.GetPostByIncludeAsync(id.Value);

            model.Post = post;

            model.RelatedPosts = await _servicePost.GetAllAsync(p => p.CategoryId == post.CategoryId && p.Id != id);

            if (model == null)
            {
                return BadRequest();
            }

            return View(model);
        }
    }
}
