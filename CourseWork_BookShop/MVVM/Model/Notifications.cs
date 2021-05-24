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

        public int? Notification_OrderID { get; set; }

        public string NotificationText { get; set; }
    }
}
