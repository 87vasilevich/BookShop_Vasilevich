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
        public HomeViewModel HomeVM { get; set; }

        public RelayCommand BankCardViewCommand { get; set; }
        public BankCardViewModel BankCardVM { get; set; }

        public RelayCommand PersonalCabinetViewCommand { get; set; }
        public PersonalCabinetViewModel PersonalCabinetVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public MainViewModel(int _userID)
        {
            HomeVM = new HomeViewModel(this);
            PersonalCabinetVM = new PersonalCabinetViewModel(this, _userID);
            BankCardVM = new BankCardViewModel();
            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            PersonalCabinetViewCommand = new RelayCommand(o =>
            {
                CurrentView = PersonalCabinetVM;
            });

            BankCardViewCommand = new RelayCommand(o =>
            {
                CurrentView = BankCardVM;
            });
        }
    }
}
