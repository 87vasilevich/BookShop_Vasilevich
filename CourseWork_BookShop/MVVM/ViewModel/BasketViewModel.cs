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

        public BasketViewModel(MainViewModel mainVM, int _userID)
        {
            int temp_userID = _userID;
            AllfromBasket = Get_allfromBasket(temp_userID);
        }

        private ObservableCollection<Basket> Get_allfromBasket(int temp_userID) //Вывод корзины
        {
            return new ObservableCollection<Basket>(basket_db.GetDataList().Where(o => o.Basket_UserID == temp_userID).ToList());
        }
    }
}
