﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseWork_BookShop.MVVM.Model;
using CourseWork_BookShop.Core;

namespace CourseWork_BookShop.MVVM.ViewModel
{
    class UserSignUpViewModel : ObservableObject
    {
        public RelayCommand Go_to_sign_in { get; set; }
        public RelayCommand Go_sign_up_again { get; set; }


        private IRepository<Users> users_db = new SQLUserRepository();
        private IRepository<Bank_Cards> card_db = new SQLCardRepository();

#region Создание пользователя
        //Переменные для регистрации
        public string _password;
        public string _password_chek;
        public string _name;
        public string _surname;
        public string _otchestvo;
        public string _login;
        public string _email;
        public string _card_number;
        public string _mm;
        public string _gg;
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

        public string Password_chek
        {
            get { return _password_chek; }
            set
            {
                _password_chek = value;
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

        public string Card_number
        {
            get { return _card_number; }
            set
            {
                _card_number = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public UserSignUpViewModel(U_Sign_MainViewModel window_main_sign)
        {
            Go_to_sign_in = new RelayCommand(o =>
            {
                window_main_sign.CurrentView = new UserSignInViewModel(window_main_sign);
            }
            );

            Go_sign_up_again = new RelayCommand(o =>
            {
                window_main_sign.CurrentView = new UserSignUpViewModel(window_main_sign);
            }
            );
        }
    }
}
