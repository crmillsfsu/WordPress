using Library.WordPress.Models;
using Library.WordPress.Services;
using System;

namespace CLI.WordPress
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to WordPress!");
            List<Blog?> blogPosts = BlogServiceProxy.Current.Blogs;
            bool cont = true;
            do
            {
                Console.WriteLine("C. Create a Blog Post");
                Console.WriteLine("R. List all Blog Posts");
                Console.WriteLine("U. Update a Blog Post");
                Console.WriteLine("D. Delete a Blog Post");
                Console.WriteLine("Q. Quit");

                var userChoice = Console.ReadLine();
                switch (userChoice)
                {
                    case "C":
                    case "c":
                        var blog = new Blog();
                        blog.Title = Console.ReadLine();
                        blog.Content = Console.ReadLine();
                        BlogServiceProxy.Current.AddOrUpdate(blog);
                        break;
                    case "R": 
                    case "r":
                        BlogServiceProxy.Current.Blogs.ForEach(Console.WriteLine);
                        break;
                    case "U": 
                    case "u":
                        {
                            BlogServiceProxy.Current.Blogs.ForEach(Console.WriteLine);
                            Console.WriteLine("Blog to Update (Id):");
                            var selection = Console.ReadLine();
                            if (int.TryParse(selection, out int intSelection))
                            {
                                //get blog object
                                var blogToUpdate = blogPosts
                                    .Where(b => b != null)
                                    .FirstOrDefault(b => (b?.Id ?? -1) == intSelection);
                                //update it!
                                if (blogToUpdate != null)
                                {
                                    blogToUpdate.Title = Console.ReadLine();
                                    blogToUpdate.Content = Console.ReadLine();
                                }
                                BlogServiceProxy.Current.AddOrUpdate(blogToUpdate);
                            }
                            
                            break;
                        }
                    case "D": 
                    case "d":
                        {
                            //display noptions to delete
                            blogPosts.ForEach(Console.WriteLine);
                            Console.WriteLine("Blog to Delete (Id):");
                            //get the blog user wants to delete
                            var selection = Console.ReadLine();
                            if(int.TryParse(selection, out int intSelection))
                            {
                                BlogServiceProxy.Current.Delete(intSelection);
                            }

                            break;
                        }
                    case "Q": 
                    case "q":
                        cont = false;
                        break;
                    default:
                        Console.WriteLine("Invalid command");
                        break;
                }

            } while (cont);
        }
    }
}