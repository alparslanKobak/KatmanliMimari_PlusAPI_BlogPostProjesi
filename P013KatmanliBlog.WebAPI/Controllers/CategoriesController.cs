using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.Service.Abstract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace P013KatmanliBlog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IService<Category> _service;

        public CategoriesController(IService<Category> service)
        {
            _service = service;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<IEnumerable<Category>> Get()
        {
            return await _service.GetAllAsync();
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public async Task<Category> Get(int id)
        {
            return await _service.FindAsync(id);
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public async Task<int> Post([FromBody] Category value)
        {
            await _service.AddAsync(value);


            return await _service.SaveAsync(); 

        }

        // PUT api/<CategoriesController>/5
        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody] Category value)
        {
            _service.Update(value);
            int sonuc = await _service.SaveAsync();
            if (sonuc>0)
            {
                return Ok(value);

            }

            return StatusCode(304);
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Category category = await _service.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }
            _service.Delete(category);

            await _service.SaveAsync();

            return Ok(category);
        }
    }
}
