using CourseWork_BookShop.Core;
using CourseWork_BookShop.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork_BookShop.MVVM.ViewModel
{
    class PersonalCabinetViewModel : ObservableObject
    {
        private IRepository<Users> users_db = new SQLUserRepository();
        private IRepository<Bank_Cards> card_db = new SQLCardRepository();

        //Переменные для вывода информации
        public string _password;
        public string _name;
        public string _surname;
        public string _otchestvo;
        public string _login;
        public string _email;
        public string _card_number;
        public string _balance;
        public string _city;
        public string _street;
        public string _house;
        public string _apartament;

        //Свойства
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                OnPropertyChanged();
            }
        }

        public string Otchestvo
        {
            get { return _otchestvo; }
            set
            {
                _otchestvo = value;
                OnPropertyChanged();
            }
        }

        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public string Card_number
        {
            get { return _card_number; }
            set
            {
                _card_number = value;
                OnPropertyChanged();
            }
        }

        public string Balance
        {
            get { return _balance; }
            set
            {
                _balance = value;
                OnPropertyChanged();
            }
        }

        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                OnPropertyChanged();
            }
        }

        public string Street
        {
            get { return _street; }
            set
            {
                _street = value;
                OnPropertyChanged();
            }
        }

        public string House
        {
            get { return _house; }
            set
            {
                _house = value;
                OnPropertyChanged();
            }
        }

        public string Apartament
        {
            get { return _apartament; }
            set
            {
                _apartament = value;
                OnPropertyChanged();
            }
        }
        public PersonalCabinetViewModel(MainViewModel mainVM, int _userID)
        {
            //Otchestvo = users_db.GetElement(_userID).Bank_Cards.Last().; дл
            Name = users_db.GetElement(_userID).UserName;
             Surname = users_db.GetElement(_userID).UserSurname;
             Otchestvo = users_db.GetElement(_userID).Bank_Cards.Last().;
        }
    }
}
