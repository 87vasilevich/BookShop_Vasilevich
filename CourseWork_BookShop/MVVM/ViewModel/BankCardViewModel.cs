using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseWork_BookShop.Core;
using CourseWork_BookShop.MVVM.Model;
using System.Windows;

namespace CourseWork_BookShop.MVVM.ViewModel
{
    class BankCardViewModel : ObservableObject
    {
        private IRepository<Users> users_db = new SQLUserRepository();
        private IRepository<Bank_Cards> cards_db = new SQLCardRepository();

        #region Сведения о банковской карте
        //Переменные
        public Bank_Cards cur_card;
        public string _card_number;
        public string _balance;
        //public string _userid;

        //Свойства
        public Bank_Cards CurrentCard
        {
            get { return cur_card; }
            set
            {
                cur_card = value;
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

        //Команда
        public RelayCommand ChangeBalance { get; set; }
        #endregion

        public BankCardViewModel (MainViewModel mainVM, int _userID)
        {
            int temp_cardid = users_db.GetElement(_userID).Bank_Cards.Last().CardID;

            CurrentCard = cards_db.GetElement(temp_cardid);

            Card_number = CurrentCard.CardNumber;
            Balance = CurrentCard.CardBalance.ToString();

            ChangeBalance = new RelayCommand(o =>
            {
                MessageBoxResult message;

                if (Balance!= null && Balance!="")
                {
                    message = MessageBox.Show("Вы уверены?", "Изменение баланса", MessageBoxButton.YesNo);
                    if (message == MessageBoxResult.Yes)
                    {
                        CurrentCard.CardBalance = float.Parse(Balance);
                        cards_db.Update(CurrentCard);
                        cards_db.Save();

                        MessageBox.Show("Баланс успешно изменён!", "Изменение баланса");
                    }
                }
                else
                {
                    MessageBox.Show("ОШИБКА!\nПроверьте введённые данные.", "Изменение баланса");
                }
            }
            );
        }
    }
}
