using System;
using CourseWork_BookShop.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork_BookShop.MVVM.ViewModel
{
    class U_Sign_MainViewModel : ObservableObject
    {
        public Action CloseAction { get; set; }

        public RelayCommand UserSignINViewCommand { get; set; } //Вход
        public UserSignInViewModel UserSignINVM { get; set; }

        public RelayCommand UserSignUPViewCommand { get; set; } //Регистрация
        public UserSignUpViewModel UserSignUPVM { get; set; }


        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public U_Sign_MainViewModel()
        {
            UserSignINVM = new UserSignInViewModel(this);
            UserSignUPVM = new UserSignUpViewModel(this);

            CurrentView = UserSignINVM;

            UserSignINViewCommand = new RelayCommand(o =>
            {
                CurrentView = UserSignINVM;
            });

            UserSignUPViewCommand = new RelayCommand(o =>
            {
                CurrentView = UserSignUPVM;
            });   
        }
    }
}
