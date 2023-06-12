using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.Service.Abstract;

namespace P013KatmanliBlog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IService<User> _service;

        public UserController(IService<User> service)
        {
            _service = service;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<IEnumerable<User>> Get() // Eldeki tüm verileri getiren Json metodu
        {
            return await _service.GetAllAsync(); 
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<User> Get(int id)
        {
            return await _service.FindAsync(id);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task Post([FromBody] User value)
        {
            await _service.AddAsync(value);
            await _service.SaveAsync();
        }

        // PUT api/<UserController>/5
        [HttpPut]
        public async Task Put(int id, [FromBody] User value)
        {
            _service.Update(value);
            await _service.SaveAsync();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            User kayit = await _service.FindAsync(id);

            if (kayit == null)
            {
                return NotFound();
            }

            _service.Delete(kayit);
            await _service.SaveAsync();
            return Ok(kayit);
        }
    }
}
