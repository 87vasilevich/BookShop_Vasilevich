using CourseWork_BookShop.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseWork_BookShop.Core;
using System.Text.RegularExpressions;

namespace CourseWork_BookShop.MVVM.ViewModel
{
    class UserSignInViewModel : ObservableObject
    {
        private IRepository<Users> users_db = new SQLUserRepository();
        //private IRepository<Bank_Cards> card_db = new SQLCardRepository();

        #region Вход пользователя
        //Переменные для входа
        public string _password;
        public string _login;

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

        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }



        //Функция по нахождению пользователя
        private int FindUser(Users tempUser, ICollection<Users> Users_list)
        {
            foreach (Users u in Users_list)
            {
                if (u.UserPassword == tempUser.UserPassword && u.UserLogin == tempUser.UserLogin)
                {
                    return u.UserID;
                }
                    
            }
            return 0;
        }

        //Команда
        public RelayCommand UserSignInSuccess { get; set; }
        #endregion

        //#region Валидация
        //public string this[string columnName]
        //{
        //    get
        //    {
        //        string error = String.Empty;
        //        switch (columnName)
        //        {
        //            case "Login":
        //                if (Login.Length == 0)
        //                {
        //                    error = "Введите логин!";
        //                }
        //                else
        //                {
        //                    error = String.Empty;
        //                    if (!IsValidLogin(Login) || Login.Contains(" "))
        //                    {
        //                        error = "Некорректный логин! Разрешено: буквы и цифры.";
        //                    }
        //                    else
        //                    {
        //                        error = String.Empty;
        //                        if (Login.Length < 4 || Login.Length > 20)
        //                        {
        //                            error = "Введите логин от 4 до 20 символов!";
        //                        }
        //                        else
        //                        {
        //                            error = String.Empty;
        //                        }
        //                    }
        //                }
        //                break;
        //            //---------------------------------------------------------
        //            case "Password":
        //                if (Password.Length == 0)
        //                {
        //                    error = "Введите пароль!";
        //                }
        //                else
        //                {
        //                    error = String.Empty;
        //                    if (!IsValidPassword(Password))
        //                    {
        //                        error = "Некорректный пароль!";
        //                    }
        //                    else
        //                    {
        //                        error = String.Empty;
        //                        if (Password.Length < 6 || Password.Length > 20)
        //                        {
        //                            error = "Пароль - от 6 до 20 символов!";
        //                        }
        //                        else
        //                            error = String.Empty;
        //                    }
        //                }
        //                break;
        //        }

        //        return error;
        //    }
        //}

        //static readonly string[] ValidatedProperties = { "Login", "Password"};
        //public bool IsValid
        //{
        //    get
        //    {
        //        foreach (string property in ValidatedProperties)
        //        {
        //            if (this[property] != String.Empty)
        //                return false;
        //        }

        //        return true;
        //    }
        //}

        //public string Error
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //}
        //#endregion

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
                if ((userID = FindUser(new Users(Password, Login), users_db.GetDataList().ToList())) != 0)
                {
                    MainWindow mainWindow = new MainWindow(userID);

                    mainWindow.Show();

                    window_main_sign.CloseAction();
                }

                else
                {
                    System.Windows.MessageBox.Show("Неверные данные","Авторизация");
                }
            });
        }

        #region Методы
        public static bool IsValidPassword(string password) //Проверка на пароль
        {
            string pattern = @"^\S+\S*$";
            Match isMatch = Regex.Match(password, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }

        public static bool IsValidLogin(string name) //Для логина
        {
            //string pattern = @"^[A-z | А-я | \d ]+[A-z | А-я | \d ]*$";
            string pattern = @"^\w+$";
            Match isMatch = Regex.Match(name, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }
        #endregion
    }
}
