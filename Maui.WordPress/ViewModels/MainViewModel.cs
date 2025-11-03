using Library.WordPress.Models;
using Library.WordPress.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Maui.WordPress.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            InlineBlog = new BlogViewModel();
            InlineCardVisibility = Visibility.Collapsed;
            ImportPath = @"C:\temp\data.json";
        }
        public ObservableCollection<BlogViewModel?> Blogs
        {
            get
            {
                return new ObservableCollection<BlogViewModel?>
                    (BlogServiceProxy
                    .Current
                    .Blogs
                    .Where(
                        b => (b?.Title?.ToUpper()?.Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                        || (b?.Content?.ToUpper()?.Contains(Query?.ToUpper() ?? string.Empty) ?? false)
                    )
                    .Select (b => new BlogViewModel (b))
                    );
            }
        }

        private Visibility inlineCardVisibility;
        public Visibility InlineCardVisibility
        {
            get
            {
                return inlineCardVisibility;
            }

            set
            {
                if (inlineCardVisibility != value)
                {
                    inlineCardVisibility = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public void Refresh()
        {
            NotifyPropertyChanged(nameof(Blogs));
        }

        public void Export()
        {
            var blogString = JsonConvert.SerializeObject(
                Blogs
                .Where(b => b!= null)
                .Select(b => b.Model));

            using (StreamWriter sw = new StreamWriter(@"C:\temp\data.json"))
            {
                sw.WriteLine(blogString);
            }
        }

        public void Import()
        {
            using(StreamReader sr = new StreamReader(ImportPath))
            {
                var blogString = sr.ReadLine();
                if(string.IsNullOrEmpty(blogString))
                {
                    return;
                }

                var blogs = JsonConvert.DeserializeObject<List<Blog>>(blogString);

                foreach (var blog in blogs)
                {
                    blog.Id = 0;
                    BlogServiceProxy.Current.AddOrUpdate(blog);
                }
                NotifyPropertyChanged(nameof(Blogs));
            }

            //var blogString = File.ReadAllText(ImportPath);
            
        }

        public string ImportPath { get; set; }
        public BlogViewModel? SelectedBlog { get; set; }
        public string? Query { get; set; }

        public BlogViewModel? InlineBlog { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void Delete()
        {
            if(SelectedBlog == null)
            {
                return;
            }

            BlogServiceProxy.Current.Delete(SelectedBlog?.Model?.Id ?? 0);
            NotifyPropertyChanged(nameof(Blogs));
        }

        public async Task<bool> AddInlineBlog()
        {
            try
            {
                await BlogServiceProxy.Current.AddOrUpdate(InlineBlog?.Model);
                NotifyPropertyChanged(nameof(Blogs));

                InlineBlog = new BlogViewModel();
                NotifyPropertyChanged(nameof(InlineBlog));
            } catch(Exception e)
            {
                return false;
            }

            return true;
        }

        public void ExpandCard()
        {
            InlineCardVisibility
                = InlineCardVisibility == Visibility.Visible ?
                Visibility.Collapsed : Visibility.Visible;
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
