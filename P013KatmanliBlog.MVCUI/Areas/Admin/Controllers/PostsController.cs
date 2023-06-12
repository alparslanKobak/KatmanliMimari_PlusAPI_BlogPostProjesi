using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.MVCUI.Utils;
using P013KatmanliBlog.Service.Abstract;
using System.Drawing.Drawing2D;

namespace P013KatmanliBlog.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "AdminPolicy")]
    public class PostsController : Controller
    {
        private readonly IPostService _service; 
        private readonly IService<Category> _serviceCategory;
        private readonly IService<User> _serviceUser;

        public PostsController(IPostService service, IService<Category> serviceCategory, IService<User> serviceUser)
        {
            _service = service;
            _serviceCategory = serviceCategory;
            _serviceUser = serviceUser;
        }

        // GET: PostsController
        public async Task<ActionResult> Index()
        {
            IEnumerable<Post> model = await _service.GetPostsByIncludeAsync();

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
            var data = await _serviceCategory.GetAllAsync();
            ViewBag.CategoryId = new SelectList(data, "Id", "Name");

            var data1 = await _serviceUser.GetAllAsync();
            ViewBag.UserId = new SelectList(data1, "Id", "Name");
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
             
                
                await _service.AddAsync(collection);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                var data = await _serviceCategory.GetAllAsync();
                ViewBag.CategoryId = new SelectList(data, "Id", "Name");

                var data1 = await _serviceUser.GetAllAsync();
                ViewBag.UserId = new SelectList(data1, "Id", "Name");
                return View();
            }
        }

        // GET: PostsController/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null) // id gönderilmeden direkt edit sayfası açılırsa 
            {
                return BadRequest(); // geriye geçersiz istek hatası dön
            }

            var model = await _service.GetPostByIncludeAsync(id.Value); // Yukarıdaki id'yi ? ile nullable yaparsak 

            if (model == null)
            {
                return NotFound(); // kayıt bulunamadı
            }
            var data = await _serviceCategory.GetAllAsync();
            ViewBag.CategoryId = new SelectList(data, "Id", "Name");

            var data1 = await _serviceUser.GetAllAsync();
            ViewBag.UserId = new SelectList(data1, "Id", "Name");

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
                _service.Update(collection);
                await _service.SaveAsync();

                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                var data = await _serviceCategory.GetAllAsync();
                ViewBag.CategoryId = new SelectList(data, "Id", "Name");

                var data1 = await _serviceUser.GetAllAsync();
                ViewBag.UserId = new SelectList(data1, "Id", "Name");
                return View(e.Message);
            }
        }

        // GET: PostsController/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null) // id gönderilmeden direkt edit sayfası açılırsa 
            {
                return BadRequest(); // geriye geçersiz istek hatası dön
            }

            var model = await _service.GetPostByIncludeAsync(id.Value); // Yukarıdaki id'yi ? ile nullable yaparsak 

            if (model == null)
            {
                return NotFound(); // kayıt bulunamadı
            }
            var data = await _serviceCategory.GetAllAsync();
            ViewBag.CategoryId = new SelectList(data, "Id", "Name");

            var data1 = await _serviceUser.GetAllAsync();
            ViewBag.UserId = new SelectList(data1, "Id", "Name");

            return View(model);
           
        }

        // POST: PostsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Post collection)
        {
            try
            {
                _service.Delete(collection);
                _service.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
