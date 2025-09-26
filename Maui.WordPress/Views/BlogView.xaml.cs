using Library.WordPress.Models;
using Library.WordPress.Services;

namespace Maui.WordPress.Views;

public partial class BlogView : ContentPage
{
	public BlogView()
	{
		InitializeComponent();
        BindingContext = new Blog();
	}

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void OkClicked(object sender, EventArgs e)
    {
        //add the blog
        BlogServiceProxy.Current.AddOrUpdate(BindingContext as Blog);

        //go back to the main page
        Shell.Current.GoToAsync("//MainPage");
    }
}