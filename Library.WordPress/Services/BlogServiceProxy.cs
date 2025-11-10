using System;
using System.ComponentModel;
using Library.WordPress.DTO;
using Library.WordPress.Models;
using Library.WordPress.Utilities;
using Newtonsoft.Json;

namespace Library.WordPress.Services;

public class BlogServiceProxy
{
    private List<BlogDTO?> blogPosts;
    private BlogServiceProxy()
    {
        blogPosts = new List<BlogDTO?>();
        var blogsResponse = new WebRequestHandler().Get("/Blog").Result;
        if (blogsResponse != null)
        {
            blogPosts = JsonConvert.DeserializeObject<List<BlogDTO?>>(blogsResponse) ?? new List<BlogDTO?>();
        }
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

    public List<BlogDTO?> Blogs
    {
        get
        {
            return blogPosts;
        }
    }

    public async Task<BlogDTO?> AddOrUpdate(BlogDTO? blog)
    {
        if (blog == null)
        {
            return null;
        }

        var blogPayload = await new WebRequestHandler().Post("/Blog", blog);
        var blogFromServer = JsonConvert.DeserializeObject<BlogDTO>(blogPayload);

        if (blog.Id <= 0)
        {
            blogPosts.Add(blogFromServer);
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

    public BlogDTO? Delete(int id)
    {
        var response = new WebRequestHandler().Delete($"/Blog/{id}").Result;
        //get blog object
        var blogToDelete = blogPosts
            .Where(b => b != null)
            .FirstOrDefault(b => (b?.Id ?? -1) == id);
        //delete it!
        blogPosts.Remove(blogToDelete);

        return blogToDelete;
    }
}
