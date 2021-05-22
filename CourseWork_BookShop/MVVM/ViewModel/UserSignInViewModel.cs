using CourseWork_BookShop.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseWork_BookShop.Core;

namespace CourseWork_BookShop.MVVM.ViewModel
{
    class UserSignInViewModel : ObservableObject
    {
        private IRepository<Users> users_db = new SQLUserRepository();
        private IRepository<Bank_Cards> card_db = new SQLCardRepository();

        #region Вход пользователя
        //Переменные для входа
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



        //Функция по нахождению пользователя
        private int FindUser(Users tempUser, ICollection<Users> Users_list)
        {
            foreach (Users u in Users_list)
            {
                if (u.UserPassword == tempUser.UserPassword && u.UserLogin== tempUser.UserLogin)
                {
                    u.UserName = tempUser.UserName;
                    u.UserEmail = tempUser.UserEmail;
                    u.UserSurname = tempUser.UserSurname;
                    u.UserOtchestvo = tempUser.UserOtchestvo;
                    u.UserCity = tempUser.UserCity;
                    u.UserStreet = tempUser.UserStreet;
                    u.UserHouse_number = tempUser.UserHouse_number;
                    u.UserApartament_number = tempUser.UserApartament_number;
                }
                    return u.UserID;
            }

            return 0;
        }

        //Команда
        public RelayCommand UserSignInSuccess { get; set; }
        #endregion



        public RelayCommand Go_to_sign_up { get; set; }

        public RelayCommand Go_sign_in_again { get; set; }


        public UserSignInViewModel(U_Sign_MainViewModel window_main_sign)
        {
            Go_to_sign_up = new RelayCommand(o =>
                {
                    window_main_sign.CurrentView = new UserSignUpViewModel(window_main_sign);
                }
            );

            Go_sign_in_again = new RelayCommand(o =>
            {
                window_main_sign.CurrentView = new UserSignInViewModel(window_main_sign);
            }
            );

            UserSignInSuccess = new RelayCommand(o =>
            {
                int userID = 0;
                if ((userID = FindUser(new Users(Password, Name, Surname, Otchestvo, City, Street, Convert.ToInt32(House), Convert.ToInt32(Apartament), Login, Email), users_db.GetDataList().ToList())) != 0)
                {
                    MainWindow mainWindow = new MainWindow(userID);

                    mainWindow.Show();

                    window_main_sign.CloseAction();
                }

                else
                {
                    System.Windows.MessageBox.Show("Неверные данные");
                }
            });
        }
    }
}
