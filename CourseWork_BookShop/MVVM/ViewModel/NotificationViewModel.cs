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
    class NotificationViewModel : ObservableObject
    {
        //---------------------------------------------------------
        private IRepository<Notifications> notification_db = new SQLNotificationRepository();
        public ObservableCollection<Notifications> allfromNotifications { get; set; }
        public ObservableCollection<Notifications> AllfromNotifications
        {
            get { return allfromNotifications; }
            set
            {
                allfromNotifications = value;
                OnPropertyChanged();
            }
        }
        //---------------------------------------------------------

        public NotificationViewModel(MainViewModel mainVM, int _userID)
        {
            AllfromNotifications = Get_allfromBasket(_userID);
        }

        private ObservableCollection<Notifications> Get_allfromBasket(int temp_userID) //Вывод корзины
        {
            return new ObservableCollection<Notifications>(notification_db.GetDataList().Where(o => o.Notification_UserID == temp_userID).ToList());
        }
    }
}
