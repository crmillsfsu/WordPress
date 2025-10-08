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
        public ObservableCollection<BlogViewModel?> Blogs
        {
            get
            {
                return new ObservableCollection<BlogViewModel?>
                    (BlogServiceProxy
                    .Current
                    .Blogs
                    .Select (b => new BlogViewModel (b))
                    );
            }
        }

        public void Refresh()
        {
            NotifyPropertyChanged(nameof(Blogs));
        }
        public BlogViewModel? SelectedBlog { get; set; }

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

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
