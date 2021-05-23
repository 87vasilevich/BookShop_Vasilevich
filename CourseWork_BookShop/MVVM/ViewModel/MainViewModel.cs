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
        public RelayCommand HomeViewCommand { get; set; }

        public RelayCommand BankCardViewCommand { get; set; }

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

            BankCardViewCommand = new RelayCommand(o =>
            {
                CurrentView = new BankCardViewModel(this, _userID);
            });
        }
    }
}
