using Library.WordPress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.WordPress.ViewModels
{
    public class MainViewModel
    {
        public List<Blog> Blogs
        {
            get
            {
                return new List<Blog>
                {
                    new Blog { Id = 1, Title = "Blog 1", Content = "Something great" }
                    ,
                    new Blog { Id = 2, Title = "Blog 2", Content = "Something great" }
                    ,
                    new Blog { Id = 3, Title = "Blog 3", Content = "Something great" }
                    ,
                    new Blog { Id = 4, Title = "Blog 4", Content = "Something great" }
                };
            }
        }

        public Blog? SelectedBlog { get; set; }
    }
}
