using Api.WordPress.Database;
using Library.WordPress.Models;

namespace Api.WordPress.Enterprise
{
    public class BlogEC
    {
        public IEnumerable<Blog> GetBlogs()
        {
            return FakeDatabase.Blogs;
        }
        public Blog? GetById(int id)
        {
            return FakeDatabase.Blogs.FirstOrDefault(b => b.Id == id);
        }

        public Blog? Delete(int id)
        {
            var toRemove = GetById(id);
            if (toRemove != null)
            {
                FakeDatabase.Blogs.Remove(toRemove);
            }
            return toRemove;
        }
    }
}
