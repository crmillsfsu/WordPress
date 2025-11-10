using Library.WordPress.DTO;
using Library.WordPress.Models;
using Library.WordPress.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Maui.WordPress.ViewModels
{
    public class BlogViewModel
    {
        public BlogViewModel() {
            Model = new BlogDTO();
            SetUpCommands();
        }

        public BlogViewModel(BlogDTO? model)
        {
            Model = model;
            SetUpCommands();
        }

        private void SetUpCommands()
        {
            DeleteCommand = new Command(DoDelete);
            EditCommand = new Command((p) => DoEdit(p as BlogViewModel));
        }

        private void DoDelete()
        {
            if (Model?.Id > 0)
            {
                BlogServiceProxy.Current.Delete(Model.Id);
                Shell.Current.GoToAsync("//MainPage");
            }
        }

        private void DoEdit(BlogViewModel? bv)
        {
            if (bv == null)
            {
                return;
            }
            var selectedId = bv?.Model?.Id ?? 0;
            Shell.Current.GoToAsync($"//Blog?blogId={selectedId}");
        }

        public BlogDTO? Model { get; set; }


        public ICommand? DeleteCommand { get; set; }
        public ICommand? EditCommand { get; set; }
    }
}
