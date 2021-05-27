using CourseWork_BookShop.Core;
using CourseWork_BookShop.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork_BookShop.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public Action CloseAction { get; set; }

        public RelayCommand HomeViewCommand { get; set; }

        public RelayCommand BasketViewCommand { get; set; }

        public RelayCommand NotificationViewCommand { get; set; }

        public RelayCommand ExitViewCommand { get; set; }

        public RelayCommand PersonalCabinetViewCommand { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public MainViewModel(int _userID)
        {
            CurrentView = new HomeViewModel(this, _userID);

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = new HomeViewModel(this, _userID);
            });

            PersonalCabinetViewCommand = new RelayCommand(o =>
            {
                CurrentView = new PersonalCabinetViewModel(this, _userID);
            });

            BasketViewCommand = new RelayCommand(o =>
            {
                CurrentView = new BasketViewModel(this, _userID);
            });

            NotificationViewCommand = new RelayCommand(o =>
            {
                CurrentView = new NotificationViewModel(this, _userID);
            });

            ExitViewCommand = new RelayCommand(o =>
            {
                User_Sign_MainWindow user_Sign_Main = new User_Sign_MainWindow();
                user_Sign_Main.Show();

                CloseAction();
            });
        }
    }
}
