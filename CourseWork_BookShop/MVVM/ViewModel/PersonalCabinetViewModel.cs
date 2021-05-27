using CourseWork_BookShop.Core;
using CourseWork_BookShop.MVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace CourseWork_BookShop.MVVM.ViewModel
{
    class PersonalCabinetViewModel : ObservableObject, IDataErrorInfo
    {
        private IRepository<Users> users_db = new SQLUserRepository();

        #region Вывод/изменение информации
        //Переменные для вывода информации
        public string _password;
        public string _password2;
        public string _newpassword;
        public string _newpasswordcheck;
        public string _name;
        public string _surname;
        public string _otchestvo;
        public string _login;
        public string _email;
        public string _city;
        public string _street;
        public string _house;
        public string _apartament;
        public string _userid;
        public Users cur_user;

        //Свойства
        public Users CurrentUser
        {
            get { return cur_user; }
            set
            {
                cur_user = value;
                OnPropertyChanged();
            }
        }
        //---------------------------
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public string Password2
        {
            get { return _password2; }
            set
            {
                _password2 = value;
                OnPropertyChanged();
            }
        }

        public string NewPassword
        {
            get { return _newpassword; }
            set
            {
                _newpassword = value;
                OnPropertyChanged();
            }
        }

        public string NewPasswordCheck
        {
            get { return _newpasswordcheck; }
            set
            {
                _newpasswordcheck = value;
                OnPropertyChanged();
            }
        }
        //--------------------------
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

        public string UserID
        {
            get { return _userid; }
            set
            {
                _userid = value;
                OnPropertyChanged();
            }
        }

        //Команды
        public RelayCommand ChangePassword { get; set; }
        public RelayCommand ChangeLogin{ get; set; }
        public RelayCommand ChangePersonalInfo { get; set; }
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
                            if (!IsValidLogin(Login) || Login.Contains(" "))
                            {
                                error = "Некорректный логин! Разрешено: буквы и цифры.";
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
                    case "NewPassword":
                        if (NewPassword.Length == 0)
                        {
                            error = String.Empty;
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
                                if (Password.Length < 6 || Password.Length > 20)
                                {
                                    error = "Пароль - от 6 до 20 символов!";
                                }
                                else
                                    error = String.Empty;
                            }
                        }
                        break;
                    //---------------------------------------------------------
                    case "NewPasswordCheck":
                        if (NewPasswordCheck == "")
                        {
                            error = String.Empty;
                        }
                        else
                        {
                            error = String.Empty;
                            if (NewPassword != NewPasswordCheck)
                            {
                                error = "Введенные пароли не совпадают!";
                            }
                            else
                                error = String.Empty;
                        }
                        break;
                    //---------------------------------------------------------
                    case "Password2":
                        if (Password2 == "")
                        {
                            error = String.Empty;
                        }
                        else
                        {
                            error = String.Empty;
                            if (Password != Password2)
                            {
                                error = "Неверный старый пароль!";
                            }
                            else
                                error = String.Empty;
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
                            if (!IsValidString(Name) || Name.Contains(" "))
                            {
                                error = "Введите корректное имя!";
                            }
                            else
                            {
                                error = String.Empty;
                                if (Name.Length < 2 || Name.Length > 15)
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
                            if (!IsValidString(Surname) || Surname.Contains(" "))
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
                            if (!IsValidString(Otchestvo) || Otchestvo.Contains(" "))
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
                            if (!IsValidAdressName(City) || City.StartsWith(" ") || City.EndsWith(" "))
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
                            if (!IsValidAdressName(Street) || Street.StartsWith(" ") || City.EndsWith(" "))
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

        static readonly string[] ValidatedProperties = { "Login", "Email", "NewPassword", "NewPasswordCheck", "Password2", "Name", "Surname", "Otchestvo", "House",
        "Apartament", "City", "Street"};
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


        public PersonalCabinetViewModel(MainViewModel mainVM, int _userID)
        {
            Password2 = "";
            NewPassword = "";
            NewPasswordCheck = "";
            //Otchestvo = users_db.GetElement(_userID).Bank_Cards.Last().;  для банковской карты!!!
            CurrentUser = users_db.GetElement(_userID);

            Name = users_db.GetElement(_userID).UserName;
            Surname = users_db.GetElement(_userID).UserSurname;
            Otchestvo = users_db.GetElement(_userID).UserOtchestvo;
            Login = users_db.GetElement(_userID).UserLogin;
            Email = users_db.GetElement(_userID).UserEmail;
            City = users_db.GetElement(_userID).UserCity;
            Street = users_db.GetElement(_userID).UserStreet;
            House = users_db.GetElement(_userID).UserHouse_number.ToString();
            Apartament = users_db.GetElement(_userID).UserApartament_number.ToString();
            Password = users_db.GetElement(_userID).UserPassword;
            UserID = users_db.GetElement(_userID).UserID.ToString();

            ChangePassword = new RelayCommand(o =>
            {
                if (IsValid)
                {
                    MessageBoxResult message;

                    if (Password == Password2 && NewPassword == NewPasswordCheck && NewPassword != Password2)
                    {
                        message = MessageBox.Show("Вы уверены?", "Смена пароля", MessageBoxButton.YesNo);
                        if (message == MessageBoxResult.Yes)
                        {
                            CurrentUser.UserPassword = NewPassword;
                            Password = CurrentUser.UserPassword;
                            users_db.Update(CurrentUser);
                            users_db.Save();


                            MessageBox.Show("Пароль успешно изменён!", "Смена пароля");
                            Password2 = "";
                            NewPassword = "";
                            NewPasswordCheck = "";
                        }
                        else
                        {
                            if (message == MessageBoxResult.No)
                            {
                                Password2 = "";
                                NewPassword = "";
                                NewPasswordCheck = "";
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("ОШИБКА!\n1.Проверьте введённые данные.\n2.Новый пароль равен старому.", "Смена пароля");
                    }
                }
                else
                {
                    MessageBox.Show("Некорректные данные!", "Смена пароля");
                }
            }
           );

            ChangeLogin = new RelayCommand(o =>
            {
                if (IsValid)
                {
                    MessageBoxResult message;

                    if (CurrentUser.UserLogin != Login)
                    {
                        message = MessageBox.Show("Вы уверены?", "Смена логина", MessageBoxButton.YesNo);
                        if (message == MessageBoxResult.Yes)
                        {
                            CurrentUser.UserLogin = Login;
                            users_db.Update(CurrentUser);
                            users_db.Save();

                            MessageBox.Show("Логин успешно изменён!", "Смена логина");
                        }
                    }
                    else
                    {
                        MessageBox.Show("ОШИБКА!\n1.Проверьте введённые данные.\n2.Новый логин равен старому.", "Смена логина");
                    }
                }
                else
                {
                    MessageBox.Show("Некорректные данные!", "Смена логина");
                }
            }
           );

            ChangePersonalInfo = new RelayCommand(o =>
            {
                if (IsValid) //------------------Квартиру можно не писать!!!!
                {
                    MessageBoxResult message;

                    message = MessageBox.Show("Вы уверены?", "Изменение персональной информации", MessageBoxButton.YesNo);
                    if (message == MessageBoxResult.Yes)
                    {
                        CurrentUser.UserName = Name;
                        CurrentUser.UserSurname = Surname;
                        CurrentUser.UserOtchestvo = Otchestvo;
                        CurrentUser.UserCity = City;
                        CurrentUser.UserStreet = Street;
                        CurrentUser.UserHouse_number = Convert.ToInt32(House);
                        CurrentUser.UserApartament_number = Convert.ToInt32(Apartament);
                        CurrentUser.UserEmail = Email;

                        users_db.Update(CurrentUser);
                        users_db.Save();

                        MessageBox.Show("Персональная информация успешно изменена!", "Изменение персональной информации");
                    }
                }
                else
                {
                    MessageBox.Show("Некорректные данные!", "Изменение персональной информации");
                }
            }
           );
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

        public static bool IsValidLogin(string name) //Для логина
        {
            //string pattern = @"^[A-z | А-я | \d ]+[A-z | А-я | \d ]*$";
            string pattern = @"^\w+$";
            Match isMatch = Regex.Match(name, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }

        public static bool IsValidAdressName(string name) //Для городов, улиц
        {
            string pattern = @"^[A-z | А-я]+[\s|-]*[A-z | А-я]*[\s|-]*[A-z | А-я]*$";
            Match isMatch = Regex.Match(name, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }
        #endregion
    }
}
