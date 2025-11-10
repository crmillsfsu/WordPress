using Api.WordPress.Database;
using Library.WordPress.DTO;
using Library.WordPress.Models;

namespace Api.WordPress.Enterprise
{
    public class BlogEC
    {
        public IEnumerable<BlogDTO> GetBlogs()
        {
            return Filebase.Current.Blogs
                //.Where(b => b.UserId == CLAIM.UserId)
                .Select(b => new BlogDTO(b))
                .OrderByDescending(b => b.Id)
                .Take(100);
        }
        public BlogDTO? GetById(int id)
        {
            var blog = Filebase.Current.Blogs.FirstOrDefault(b => b.Id == id);
            return new BlogDTO(blog);
        }

        public BlogDTO? Delete(int id)
        {
            //var toRemove = GetById(id);
            var toRemove = Filebase.Current.Blogs.FirstOrDefault(b => b.Id == id);
            if (toRemove != null)
            {
                Filebase.Current.Blogs.Remove(toRemove);
            }
            return new BlogDTO(toRemove);
        }

        public BlogDTO? AddOrUpdate(BlogDTO? blogDTO)
        {
            if (blogDTO == null)
            {
                return null;
            }
            var blog = new Blog(blogDTO);
            blogDTO = new BlogDTO(Filebase.Current.AddOrUpdate(blog));
            return blogDTO;
        }

        public IEnumerable<BlogDTO?> Search(string query)
        {
            return Filebase.Current.Blogs.Where(
                        b => (b?.Title?.ToUpper()?.Contains(query?.ToUpper() ?? string.Empty) ?? false)
                        || (b?.Content?.ToUpper()?.Contains(query?.ToUpper() ?? string.Empty) ?? false)
                    ).Select(b => new BlogDTO(b));
        }
    }
}
