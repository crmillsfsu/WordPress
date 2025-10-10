using Library.WordPress.Models;
using Library.WordPress.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Maui.WordPress.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            InlineBlog = new BlogViewModel();
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

        public void Refresh()
        {
            NotifyPropertyChanged(nameof(Blogs));
        }
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

        public void AddInlineBlog()
        {
            BlogServiceProxy.Current.AddOrUpdate(InlineBlog?.Model);
            NotifyPropertyChanged(nameof(Blogs));

            InlineBlog = new BlogViewModel();
            NotifyPropertyChanged(nameof(InlineBlog));
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
