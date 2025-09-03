using Library.WordPress.Models;
using System;

namespace CLI.WordPress
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to WordPress!");
            List<Blog?> blogPosts = new List<Blog?>();
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
                        blogPosts.Add(blog);
                        break;
                    case "R": 
                    case "r":
                        foreach(var b in blogPosts)
                        {
                            Console.WriteLine(b);
                        }
                        break;
                    case "U": 
                    case "u":
                        break;
                    case "D": 
                    case "d":
                        break;
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