using System;
using System.ComponentModel;
using Library.WordPress.Models;

namespace Library.WordPress.Services;

public class BlogServiceProxy
{
    private List<Blog?> blogPosts;
    private BlogServiceProxy()
    {
        blogPosts = new List<Blog?>();
    }
    private static BlogServiceProxy? instance;
    private static object instanceLock = new object();
    public static BlogServiceProxy Current
    {
        get
        {
            lock(instanceLock)
            { 
                if (instance == null)
                {
                    instance = new BlogServiceProxy();
                }
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

    public Blog? AddOrUpdate(Blog? blog)
    {
        if (blog == null)
        {
            return null;
        }

        if (blog.Id <= 0)
        {
            var maxId = -1;
            if (blogPosts.Any())
            {
                maxId = blogPosts.Select(b => b?.Id ?? -1).Max();
            }
            else
            {
                maxId = 0;
            }
            blog.Id = ++maxId;
            blogPosts.Add(blog);
        }
        else
        {
            var blogToEdit = Blogs.FirstOrDefault(b => (b?.Id ?? 0) == blog.Id);
            if (blogToEdit != null)
            {
                var index = Blogs.IndexOf(blogToEdit);
                Blogs.RemoveAt(index);
                blogPosts.Insert(index, blog);
            }
        }
            return blog;
    }

    public Blog? Delete(int id)
    {
        //get blog object
        var blogToDelete = blogPosts
            .Where(b => b != null)
            .FirstOrDefault(b => (b?.Id ?? -1) == id);
        //delete it!
        blogPosts.Remove(blogToDelete);

        return blogToDelete;
    }
}
