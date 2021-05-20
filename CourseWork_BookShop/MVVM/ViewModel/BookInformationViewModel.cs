using CourseWork_BookShop.MVVM.Model;
using CourseWork_BookShop.Core;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork_BookShop.MVVM.ViewModel
{
    class BookInformationViewModel
    {
        public Books SelectedBook { get; set; }
        public void SetBook(Books selectedBook)
        {
            this.SelectedBook = selectedBook;
        }

        public RelayCommand GoBack_button { get; set; }

        public BookInformationViewModel(HomeViewModel homeVM)
        {
            GoBack_button = new RelayCommand(o =>
            {
                homeVM.mainVM.CurrentView = new HomeViewModel(homeVM.mainVM);
            });
        }
    }
}
