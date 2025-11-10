using Library.WordPress.Models;
using Library.WordPress.Services;

namespace Library.WordPress.DTO
{
    public class BlogDTO
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int Id { get; set; }

        public string Display
        {
            get
            {
                return ToString();
            }
        }
        public override string ToString()
        {
            return $"{Id}. {Title} - {Content}";
        }

        public BlogDTO(Blog blog)
        {
            Title = blog.Title;
            Content = blog.Content;
            Id = blog.Id;
        }

        public BlogDTO()
        {

        }

        public BlogDTO(int id)
        {
            var blogCopy = BlogServiceProxy.Current.Blogs.FirstOrDefault(b => (b?.Id ?? 0) == id);

            if (blogCopy != null)
            {
                Id = blogCopy.Id;
                Title = blogCopy.Title;
                Content = blogCopy.Content;
            }

        }
    }
}
