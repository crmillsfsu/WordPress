using System;
using Library.WordPress.Models;

namespace Library.WordPress.Services;

public class BlogServiceProxy
{
    private List<Blog?> blogPosts;

    private BlogServiceProxy()
    {
        blogPosts = new List<Blog?>();
    }

    public static BlogServiceProxy? instance;

    public static BlogServiceProxy Current
    {
        get
        {
            if (instance == null)
            {
                return new BlogServiceProxy();
            }
            return instance;
        }
    }

    public List<Blog?> Blogs
    {
        get
        {
            return blogPosts;
        }
    }

    public void Create(Blog? blog)
    {
        if (blog == null)
        {
            return;
        }
        var maxId = -1;
        if (blogPosts.Any())
        {
            maxId = blogPosts.Select(b => b?.Id ?? -1).Max();
        } else
        {
            maxId = 0;
        }
        blog.Id = ++maxId;
        blogPosts.Add(blog);
    }
}
