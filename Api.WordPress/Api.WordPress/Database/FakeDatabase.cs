using Library.WordPress.Models;

namespace Api.WordPress.Nonsense
{
    public static class FakeDatabase
    {
        public static List<Blog> Blogs = new List<Blog>
        {
            new Blog{Title = "First", Content="My First Blog", Id=1},
            new Blog{Title = "Second", Content="My Second Blog", Id=2},
            new Blog{Title = "Third", Content="My Third Blog", Id=3}
        };
    }
}
