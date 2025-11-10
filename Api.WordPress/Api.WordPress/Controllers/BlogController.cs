using Api.WordPress.Database;
using Api.WordPress.Enterprise;
using Library.WordPress.Models;
using Library.WordPress.Data;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using Library.WordPress.DTO;

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
        public IEnumerable<BlogDTO> Get()
        {
            return new BlogEC().GetBlogs();
        }

        [HttpGet("{id}")]
        public BlogDTO? GetById(int id)
        {
            return new BlogEC().GetById(id);
        }

        [HttpDelete("{id}")]
        public BlogDTO? Delete(int id)
        {
            return new BlogEC().Delete(id);
        }

        [HttpPost]
        public BlogDTO? AddOrUpdate([FromBody] BlogDTO blog)
        {
            return new BlogEC().AddOrUpdate(blog);
        }

        [HttpPost("Search")]
        public IEnumerable<BlogDTO?> Search([FromBody] QueryRequest query)
        {
            return new BlogEC().Search(query.Content);
        }
    }
}
