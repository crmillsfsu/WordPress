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
        public ObservableCollection<Blog?> Blogs
        {
            get
            {
                return new ObservableCollection<Blog?>(BlogServiceProxy.Current.Blogs);
            }
        }

        public void Refresh()
        {
            NotifyPropertyChanged(nameof(Blogs));
        }
        public Blog? SelectedBlog { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void Delete()
        {
            if(SelectedBlog == null)
            {
                return;
            }

            BlogServiceProxy.Current.Delete(SelectedBlog.Id);
            NotifyPropertyChanged(nameof(Blogs));
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
