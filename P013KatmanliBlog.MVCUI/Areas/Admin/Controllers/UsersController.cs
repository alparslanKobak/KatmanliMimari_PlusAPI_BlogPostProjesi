using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.MVCUI.Utils;
using P013KatmanliBlog.Service.Abstract;

namespace P013KatmanliBlog.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "AdminPolicy")] // Program.cs ekranında Poliçe olarak sunduk "AdminPolicy"
    public class UsersController : Controller
    {
        private readonly IService<User> _service;

        public UsersController(IService<User> service)
        {
            _service = service;
        }

        // GET: UsersController
        public async Task<ActionResult> Index()
        {
            IEnumerable<User> model = await _service.GetAllAsync();

            return View(model);

            
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
                if (ProfilePicture!= null)
                {
                    collection.ProfilePicture = await FileHelper.FileLoaderAsync(ProfilePicture);
                }

                await _service.AddAsync(collection);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsersController/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User model = await _service.FindAsync(id.Value);

            if (model == null)
            {
                return BadRequest();
            }
            return View(model);
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, User collection, IFormFile? ProfilePicture)
        {
            try
            {
                if (ProfilePicture != null)
                {
                    collection.ProfilePicture = await FileHelper.FileLoaderAsync(ProfilePicture);
                }
                _service.Update(collection);
                await _service.SaveAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsersController/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User model = await _service.FindAsync(id.Value);

            if (model == null)
            {
                return BadRequest();
            }
            return View(model);
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, User collection)
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
