using CourseWork_BookShop.Core;
using CourseWork_BookShop.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CourseWork_BookShop.MVVM.ViewModel
{
    class PersonalCabinetViewModel : ObservableObject
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


        public PersonalCabinetViewModel(MainViewModel mainVM, int _userID)
        {
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
                MessageBoxResult message;

                if(Password == Password2 && NewPassword == NewPasswordCheck && Password2!="" && Password2!=null && NewPassword != "" && NewPassword != null &&
                NewPasswordCheck != "" && NewPasswordCheck != null && Password!=NewPassword)
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
                        if(message == MessageBoxResult.No)
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
           );

            ChangeLogin = new RelayCommand(o =>
            {
                MessageBoxResult message;

                if (CurrentUser.UserLogin != Login && Login!=null && Login!="")
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
           );

            ChangePersonalInfo = new RelayCommand(o =>
            {
                MessageBoxResult message;

                //Часть кода из if
                //CurrentUser.UserName != Name && CurrentUser.UserSurname != Surname && CurrentUser.UserOtchestvo != Otchestvo &&
                //CurrentUser.UserCity != City && CurrentUser.UserStreet != Street && CurrentUser.UserHouse_number.ToString() != House &&
                //CurrentUser.UserApartament_number.ToString() != Apartament && CurrentUser.UserEmail != Email &&

                if (

                Name!=null && Name!="" && Surname!=null && Surname!="" && Otchestvo!=null && Otchestvo!="" &&
                City!= null && City !="" && Street!=null && Street!="" && House!=null && House!="" && Apartament!=null &&
                Apartament!="" && Email!=null && Email!="") //------------------Квартиру можно не писать!!!!
                {
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
                    MessageBox.Show("ОШИБКА!\nПроверьте введённые данные.", "Изменение персональной информации");
                }
            }
           );
        }
    }
}
