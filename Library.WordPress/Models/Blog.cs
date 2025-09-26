using Library.WordPress.Services;

namespace Library.WordPress.Models
{
    public class Blog
    {
        public string? Title { get; set; }
        public string? Content {  get; set; }
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

        public Blog()
        {

        }
        public Blog(int id)
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
