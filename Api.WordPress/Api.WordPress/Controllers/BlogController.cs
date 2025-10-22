using Api.WordPress.Enterprise;
using Library.WordPress.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.WordPress.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly ILogger<BlogController> _logger;

        public BlogController(ILogger<BlogController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Blog> Get()
        {
            return new BlogEC().GetBlogs();
        }

        [HttpGet("{id}")]
        public Blog? GetById(int id)
        {
            return new BlogEC().GetById(id);
        }

        [HttpDelete("{id}")]
        public Blog? Delete(int id)
        {
            return new BlogEC().Delete(id);
        }

    }
}
