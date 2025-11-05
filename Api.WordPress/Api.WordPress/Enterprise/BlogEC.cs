using Api.WordPress.Database;
using Library.WordPress.Models;

namespace Api.WordPress.Enterprise
{
    public class BlogEC
    {
        public IEnumerable<Blog> GetBlogs()
        {
            return FakeDatabase.Blogs
                //.Where(b => b.UserId == CLAIM.UserId)
                .OrderByDescending(b => b.Id)
                .Take(100);
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

        public Blog? AddOrUpdate(Blog? blog)
        {
            if (blog == null)
            {
                return null;
            }

            if (blog.Id <= 0)
            {
                var maxId = -1;
                if (FakeDatabase.Blogs.Any())
                {
                    maxId = FakeDatabase.Blogs.Select(b => b?.Id ?? -1).Max();
                }
                else
                {
                    maxId = 0;
                }
                blog.Id = ++maxId;
                FakeDatabase.Blogs.Add(blog);
            }
            else
            {
                var blogToEdit = FakeDatabase.Blogs.FirstOrDefault(b => (b?.Id ?? 0) == blog.Id);
                if (blogToEdit != null)
                {
                    var index = FakeDatabase.Blogs.IndexOf(blogToEdit);
                    FakeDatabase.Blogs.RemoveAt(index);
                    FakeDatabase.Blogs.Insert(index, blog);
                }
            }
            return blog;
        }

        public List<Blog?> Search(string query)
        {
            return FakeDatabase.Blogs.Where(
                        b => (b?.Title?.ToUpper()?.Contains(query?.ToUpper() ?? string.Empty) ?? false)
                        || (b?.Content?.ToUpper()?.Contains(query?.ToUpper() ?? string.Empty) ?? false)
                    );
        }
    }
}
