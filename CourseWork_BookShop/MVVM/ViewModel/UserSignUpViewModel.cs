using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseWork_BookShop.MVVM.Model;
using CourseWork_BookShop.Core;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace CourseWork_BookShop.MVVM.ViewModel
{
    class UserSignUpViewModel : ObservableObject, IDataErrorInfo
    {
        public RelayCommand Go_to_sign_in { get; set; }
        public RelayCommand Go_sign_up_again { get; set; }


        private IRepository<Users> users_db = new SQLUserRepository();
        private IRepository<Bank_Cards> card_db = new SQLCardRepository();

        #region Для создания пользователя
        //Переменные для регистрации
        public string _password = "";
        public string _password_chek = "";
        public string _name = "";
        public string _surname = "";
        public string _otchestvo = "";
        private string _login = "";
        public string _email = "";
        public string _card_number = "";
        public string _balance = "";
        public string _city = "";
        public string _street = "";
        public string _house = "";
        public string _apartament = "";

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

        public string Password_chek //*
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


        //Команда
        public RelayCommand User_sign_upCommand { get; set; }
        #endregion

        #region Валидация
        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Login":
                        if (Login.Length == 0)
                        {
                            error = "Введите логин!";
                        }
                        else
                        {
                            error = String.Empty;
                            if (!IsValidLogin(Login))
                            {
                                error = "Некорректный логин! Разрешено: буквы, _ и цифры.";
                            }
                            else
                            {
                                error = String.Empty;
                                if (Login.Length < 4 || Login.Length > 20)
                                {
                                    error = "Введите логин от 4 до 20 символов!";
                                }
                                else
                                {
                                    error = String.Empty;
                                }
                            }
                        }
                        break;
                    //---------------------------------------------------------
                    case "Email":
                        if (Email.Length == 0)
                        {
                            error = "Введите Email!";
                        }
                        else
                        {
                            error = String.Empty;
                            if (!isValidEmail(Email))
                            {
                                error = "Введите корректный адрес эл. почты!";
                            }
                            else
                                error = String.Empty;
                        }
                        break;
                    //---------------------------------------------------------
                    case "Password":
                        if (Password.Length == 0)
                        {
                            error = "Введите пароль!";
                        }
                        else
                        {
                            error = String.Empty;
                            if (!IsValidPassword(Password))
                            {
                                error = "Некорректный пароль!";
                            }
                            else
                            {
                                error = String.Empty;
                                if (Password.Length <6 || Password.Length>20)
                                {
                                    error = "Пароль - от 6 до 20 символов!";
                                }
                                else
                                    error = String.Empty;
                            }
                        }
                        break;
                    //---------------------------------------------------------
                    case "Password_chek":
                        if (Password_chek == "")
                        {
                            error = "Повторите пароль!";
                        }
                        else
                        {
                            error = String.Empty;
                            if (Password != Password_chek)
                            {
                                error = "Введенные пароли не совпадают!";
                            }
                            else
                                error = String.Empty;
                        }
                        break;
                    //---------------------------------------------------------
                    case "Card_number":
                        if (Card_number.Length == 0)
                        {
                            error = "Введите номер карты!";
                        }
                        else
                        {
                            error = String.Empty;
                            if (!IsValidCard(Card_number))
                            {
                                error = "Номер карты - 16 цифр!";
                            }
                            else
                            {
                                    error = String.Empty;
                            }
                        }
                        break;
                    //---------------------------------------------------------
                    case "Apartament":
                        if (Apartament == "")
                        {
                            error = String.Empty;
                        }
                        else
                        {
                            if (!IsValidNumber(Apartament))
                            {
                                error = "Некорректный номер квартиры!";
                            }
                            else
                            {
                                error = String.Empty;
                                if (Convert.ToInt32(Apartament) <= 0 || Convert.ToInt32(Apartament) >= 1000)
                                {
                                    error = "Номер от 1 до 999!";
                                }
                                else
                                    error = String.Empty;
                            }
                        }
                    break;
                    //---------------------------------------------------------
                    case "House":
                        if (House.Length == 0)
                        {
                            error = "Введите номер дома!";
                        }
                        else
                        {
                            error = String.Empty;
                            if (!IsValidNumber(House))
                            {
                                error = "Некорректный номер дома!";
                            }
                            else
                            {
                                error = String.Empty;
                                if (Convert.ToInt32(House) <= 0 || Convert.ToInt32(House) >= 1000)
                                {
                                    error = "Номер от 1 до 999!";
                                }
                                else
                                    error = String.Empty;
                            }
                        }
                        break;
                        
                    //---------------------------------------------------------
                    case "Name":
                        if (Name.Length == 0)
                        {
                            error = "Введите имя!";
                        }
                        else
                        {
                            error = String.Empty;
                            if (!IsValidString(Name))
                            {
                                error = "Введите корректное имя!";
                            }
                            else
                            {
                                error = String.Empty;
                                if(Name.Length <2 || Name.Length >15)
                                {
                                    error = "Введите имя от 2 до 15 символов!";
                                }
                                else
                                {
                                    error = String.Empty;
                                }
                            }
                        }
                        break;
                    //---------------------------------------------------------
                    case "Surname":
                        if (Surname.Length == 0)
                        {
                            error = "Введите фамилию!";
                        }
                        else
                        {
                            error = String.Empty;
                            if (!IsValidString(Surname))
                            {
                                error = "Введите корректное имя!";
                            }
                            else
                            {
                                error = String.Empty;
                                if (Surname.Length < 2 || Surname.Length > 20)
                                {
                                    error = "Введите фамилию от 2 до 20 символов!";
                                }
                                else
                                {
                                    error = String.Empty;
                                }
                            }
                        }
                        break;
                    //---------------------------------------------------------
                    case "Otchestvo":
                        if (Otchestvo.Length == 0)
                        {
                            error = "Введите отчество!";
                        }
                        else
                        {
                            error = String.Empty;
                            if (!IsValidString(Otchestvo))
                            {
                                error = "Введите корректное отчество!";
                            }
                            else
                            {
                                error = String.Empty;
                                if (Otchestvo.Length < 4 || Otchestvo.Length > 22)
                                {
                                    error = "Введите фамилию от 4 до 22 символов!";
                                }
                                else
                                {
                                    error = String.Empty;
                                }
                            }
                        }
                        break;
                    //---------------------------------------------------------
                    case "City":
                        if (City.Length == 0)
                        {
                            error = "Введите город!";
                        }
                        else
                        {
                            error = String.Empty;
                            if (!IsValidAdressName(City))
                            {
                                error = "Введите корректный город!";
                            }
                            else
                            {
                                error = String.Empty;
                                if (City.Length < 2 || City.Length > 20)
                                {
                                    error = "Введите город от 2 до 20 символов!";
                                }
                                else
                                {
                                    error = String.Empty;
                                }
                            }
                        }
                        break;
                    //---------------------------------------------------------
                    case "Street":
                        if (Street.Length == 0)
                        {
                            error = "Введите улицу!";
                        }
                        else
                        {
                            error = String.Empty;
                            if (!IsValidAdressName(Street))
                            {
                                error = "Введите корректную улицу!";
                            }
                            else
                            {
                                error = String.Empty;
                                if (Street.Length < 4 || Street.Length > 30)
                                {
                                    error = "Введите улицу от 4 до 30 символов!";
                                }
                                else
                                {
                                    error = String.Empty;
                                }
                            }
                        }
                        break;
                }

                return error;
            }
        }

        static readonly string[] ValidatedProperties = { "Login", "Email", "Password", "Password_сhek", "Name", "Surname", "Otchestvo", "House",
        "Apartament", "City", "Street", "Card_number"};
        public bool IsValid
        {
            get
            {
                foreach (string property in ValidatedProperties)
                {
                    if (this[property] != String.Empty)
                        return false;
                }

                return true;
            }
        }

        public string Error
        {
            get
            {
                throw new NotImplementedException();
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

            User_sign_upCommand = new RelayCommand(o =>
            {
                if (IsValid)
                {
                    //Добавление в Users
                    users_db.Create(new Users(Password, Name, Surname, Otchestvo, City, Street, Convert.ToInt32(House), Convert.ToInt32(Apartament), Login, Email));
                    users_db.Save();

                    //Добавление банковской карты
                    Random rnd = new Random();
                    Balance = (rnd.Next(1000000, 3000000)).ToString(); //Рандомный баланс

                    int tempID = users_db.GetDataList().Where(x => x.UserLogin == Login).ToList().Last().UserID;
                    card_db.Create(new Bank_Cards(tempID, Card_number, Convert.ToDouble(Balance)));
                    card_db.Save();

                    //Открытие окна для входа
                    window_main_sign.CurrentView = new UserSignInViewModel(window_main_sign);
                }
                else
                {
                    System.Windows.MessageBox.Show("Неккоректные данные!");
                }
                
            });
        }

        #region Методы
        public bool isValidEmail(string email) //Проверка на Email
        {
            string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
            Match isMatch = Regex.Match(email, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }

        public static bool IsValidPassword(string password) //Проверка на пароль
        {
            string pattern = @"^\S+\S*$";
            Match isMatch = Regex.Match(password, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }

        public static bool IsValidCard(string password) //Проверка банк. карты
        {
            string pattern = @"^[0-9]{4}[0-9]{4}[0-9]{4}[0-9]{4}$";
            Match isMatch = Regex.Match(password, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }

        public static bool IsValidNumber(string password) //Для целого числа
        {
            string pattern = @"^[1-9]+[0-9]*[0-9]*$";
            Match isMatch = Regex.Match(password, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }

        public static bool IsValidString(string password) //Для имени, фамилии и отчества
        {
            string pattern = @"^[A-z | А-я]*$";
            Match isMatch = Regex.Match(password, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }

        public static bool IsValidLogin(string password) //Для логина
        {
            string pattern = @"^[A-z | А-я | \w | _]+[A-z | А-я | \w | _]*$";
            Match isMatch = Regex.Match(password, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }

        public static bool IsValidAdressName(string password) //Для городов, улиц
        {
            string pattern = @"^[A-z | А-я]+[\s|-]*[A-z | А-я]*[\s|-]*[A-z | А-я]*$";
            Match isMatch = Regex.Match(password, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }
        #endregion
    }
}
