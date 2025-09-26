using Maui.WordPress.ViewModels;
using System.ComponentModel;

namespace Maui.WordPress
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }

        private void AddClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Blog");
        }

        private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
        {
            (BindingContext as MainViewModel).Refresh();
        }

        private void DeleteClicked(object sender, EventArgs e)
        {
            (BindingContext as MainViewModel).Delete();
        }
    }

}
