using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.Service.Abstract;

namespace P013KatmanliBlog.MVCUI.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IService<Category> _service;
        private readonly IPostService _servicePost;

        public CategoriesController(IService<Category> service, IPostService servicePost)
        {
            _service = service;
            _servicePost = servicePost;
        }

        public async Task<IActionResult> Index(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            Category model = await _service.FindAsync(id.Value);

            if (model==null)
            {
                return BadRequest();
            }

            model.Posts = await _servicePost.GetAllAsync(p=> p.CategoryId== id);

            return View(model);
        }
    }
}
