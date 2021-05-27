using CourseWork_BookShop.Core;
using CourseWork_BookShop.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork_BookShop.MVVM.ViewModel
{
    class BasketViewModel : ObservableObject
    {
        #region Оформление заказа
        private IRepository<Orders> order_db = new SQLOrderRepository();
        private IRepository<Users> users_db = new SQLUserRepository();
        private IRepository<Notifications> notification_db = new SQLNotificationRepository();

        //Переменные
        public string b_amount;
        public int _bookid;
        public double ordertotalsum;
        public string aname;
        public string asurname;
        public string bname;
        public string datestart;
        public string dateend;
        public double _finalsum;

        //Переменные для уведомлений
        public string _ntext;
        public int _oid;

        DateTime date_end;
        Random rnd = new Random();

        //Свойства
        public double FinalSum
        {
            get { return _finalsum; }
            set
            {
                _finalsum = value;
                OnPropertyChanged();
            }
        }

        public string DateEnd
        {
            get { return dateend; }
            set
            {
                dateend = value;
                OnPropertyChanged();
            }
        }

        public string DateStart
        {
            get { return datestart; }
            set
            {
                datestart = value;
                OnPropertyChanged();
            }
        }

        public int BookID
        {
            get { return _bookid; }
            set
            {
                _bookid = value;
                OnPropertyChanged();
            }
        }

        public string BName
        {
            get { return bname; }
            set
            {
                bname = value;
                OnPropertyChanged();
            }
        }

        public string AName
        {
            get { return aname; }
            set
            {
                aname = value;
                OnPropertyChanged();
            }
        }

        public string ASurname
        {
            get { return asurname; }
            set
            {
                asurname = value;
                OnPropertyChanged();
            }
        }

        public string BAmount
        {
            get { return b_amount; }
            set
            {
                b_amount = value;
                OnPropertyChanged();
            }
        }

        public double OrderTotalSum //Общая сумма одной строки в таблице Заказы
        {
            get { return ordertotalsum; }
            set
            {
                ordertotalsum = value;
                OnPropertyChanged();
            }
        }

        //Свойства для уведомления
        public string NotifText
        {
            get { return _ntext; }
            set
            {
                _ntext = value;
                OnPropertyChanged();
            }
        }

        public int OID //order id для уведомлений
        {
            get { return _oid; }
            set
            {
                _oid = value;
                OnPropertyChanged();
            }
        }

        //--------------------------------------------------------- Для уведомлений
        public ObservableCollection<Orders> allfromOrders { get; set; }
        public ObservableCollection<Orders> AllfromOrders
        {
            get { return allfromOrders; }
            set
            {
                allfromOrders = value;
                OnPropertyChanged();
            }
        }
        //---------------------------------------------------------


        //Команда
        public RelayCommand AddOrder { get; set; }
        #endregion


        //Свойство для выбора оплаты
        public string _oplata;
        public string Oplata
        {
            get { return _oplata; }
            set
            {
                _oplata = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand DeleteFromBasket { get; set; }
        


        //---------------------------------------------------------
        private IRepository<Basket> basket_db = new SQLBasketRepository();
        public ObservableCollection<Basket> allfromBasket { get; set; }
        public ObservableCollection<Basket> AllfromBasket
        {
            get { return allfromBasket; }
            set
            {
                allfromBasket = value;
                OnPropertyChanged();
            }
        }
        //---------------------------------------------------------

        //---------------------------------------------------------
        //для корзины
        public Basket currentbasket;
        public Basket CurentBasket
        {
            get { return currentbasket; }
            set
            {
                currentbasket = value;
                OnPropertyChanged();
            }
        }

        public string isempty;
        public string IsBasketEmpty
        {
            get { return isempty; }
            set
            {
                isempty = value;
                OnPropertyChanged();
            }
        }
        //---------------------------------------------------------

        public BasketViewModel(MainViewModel mainVM, int _userID)
        {
            //------------------------------------------
            int temp_userID = _userID;
            AllfromBasket = Get_allfromBasket(temp_userID);
            if(AllfromBasket.Count == 0)
            {
                IsBasketEmpty = "Корзина пуста!";
            }
            else
            {
                IsBasketEmpty = "";
            }

            //Подсчёт общей суммы заказа
            FinalSum = 0;
            foreach (Basket b in AllfromBasket)
            {
                //Получаем данные
                OrderTotalSum = (double)b.BasketTotalSum;

                //Общая сумма заказа
                FinalSum += OrderTotalSum;
            }
            //------------------------------------------



            DeleteFromBasket = new RelayCommand(o => //----------------очистка всей корзины
            {
                foreach(Basket b in AllfromBasket)
                {
                    basket_db.Delete(b.BasketID);
                }
                basket_db.Save();

                AllfromBasket = Get_allfromBasket(temp_userID);
                if (AllfromBasket.Count == 0)
                {
                    IsBasketEmpty = "Корзина пуста!";
                }

                //Подсчёт общей суммы заказа
                FinalSum = 0;
            });

            AddOrder = new RelayCommand(o => //----------------Оформление заказа
            {
                //------------------------------------------
                DateStart = DateTime.Now.ToString(); //Дата оформления

                //Работаем с датой окончания
                date_end = DateTime.Now;
                int days = rnd.Next(1, 3);
                date_end = date_end.AddDays(days);
                DateEnd = date_end.ToString();

                //Процесс оформления
                FinalSum = 0;
                foreach (Basket b in AllfromBasket) 
                {
                    //Получаем данные
                    BookID = (int)b.Basket_BookID;
                    BAmount = b.BasketBookAmount.ToString();
                    AName = b.A_Name;
                    ASurname = b.A_Surname;
                    BName = b.B_Name;
                    OrderTotalSum = (double)b.BasketTotalSum;

                    //Общая сумма заказа
                    FinalSum += OrderTotalSum;

                    order_db.Create(new Orders(AName, ASurname, BName, DateStart, DateEnd, temp_userID, BookID, OrderTotalSum, Convert.ToInt32(BAmount)));
                    order_db.Save();
                }

                //Процесс добавления уведомления
                AllfromOrders = Get_allfromOrders(DateStart);
                NotifText = "Заказ:\n";
                foreach(Orders ord in AllfromOrders)
                {
                    NotifText += $"Название: {ord.B_Name} --- Количество: {ord.OrderBookAmount}\n";
                }
                NotifText += $"Сумма заказа: {FinalSum}$\n";
                NotifText += $"Способ оплаты при получении: {Oplata}.\n";
                NotifText += $"Дата оформления заказа: {DateStart}. Дата доставки: {DateEnd}.";
                notification_db.Create(new Notifications(_userID, NotifText));
                notification_db.Save();

                //очистка корзины
                foreach (Basket b in AllfromBasket)
                {
                    basket_db.Delete(b.BasketID);
                }
                basket_db.Save();
                //------------------------------------------


                //------------------------------------------
                //Обновление корзины
                AllfromBasket = Get_allfromBasket(temp_userID);
                if (AllfromBasket.Count == 0)
                {
                    IsBasketEmpty = "Корзина пуста!";
                }

                //Подсчёт общей суммы заказа
                FinalSum = 0;
                foreach (Basket b in AllfromBasket)
                {
                    //Получаем данные
                    OrderTotalSum = (double)b.BasketTotalSum;

                    //Общая сумма заказа
                    FinalSum += OrderTotalSum;
                }
                //------------------------------------------
            });
        }

        private ObservableCollection<Basket> Get_allfromBasket(int temp_userID) //Вывод корзины
        {
            return new ObservableCollection<Basket>(basket_db.GetDataList().Where(o => o.Basket_UserID == temp_userID).ToList());
        }

        private ObservableCollection<Orders> Get_allfromOrders(string _datestart) //Вывод заказов
        {
            return new ObservableCollection<Orders>(order_db.GetDataList().Where(t => t.OrderDateStart.ToString() == _datestart).ToList());
        }
    }
}
