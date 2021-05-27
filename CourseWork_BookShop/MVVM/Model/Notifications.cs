namespace CourseWork_BookShop.MVVM.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Notifications
    {
        [Key]
        public int NotificationID { get; set; }

        public int? Notification_UserID { get; set; }

        public string NotificationText { get; set; }

        public Notifications()
        {

        }

        public Notifications(int _userid, string _text)
        {
            Notification_UserID = _userid;
            NotificationText = _text;
        }
    }
}
