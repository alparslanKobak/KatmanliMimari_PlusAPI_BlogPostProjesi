using Microsoft.AspNetCore.Mvc;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.Service.Abstract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace P013KatmanliBlog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _service;

        public PostController(IPostService service)
        {
            _service = service;
        }

        // GET: api/<PostController>
        [HttpGet]
        public async Task<IEnumerable<Post>> Get()
        {
            return await _service.GetPostsByIncludeAsync();
        }

        // GET api/<PostController>/5
        [HttpGet("{id}")]
        public async Task<Post> Get(int id)
        {
            return await _service.GetPostByIncludeAsync(id);
        }

        // POST api/<PostController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Post value)
        {
            await _service.AddAsync(value);
            await _service.SaveAsync();

            return Ok(value);
        }

        // PUT api/<PostController>/5
        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody] Post value)
        {
            _service.Update(value);
           int sonuc = await _service.SaveAsync();
            if (sonuc>0)
            {
                return Ok(value);
            }

            return Problem();
        }

        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Post data = await _service.FindAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            int sonuc = await _service.SaveAsync();

            if (sonuc > 0)
            {
                return Ok(data);
            }

            return Problem();
        }
    }
}
