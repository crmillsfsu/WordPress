using Maui.WordPress.ViewModels;

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

    }

}
