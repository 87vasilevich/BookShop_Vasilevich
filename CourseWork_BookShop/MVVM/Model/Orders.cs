namespace CourseWork_BookShop.MVVM.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Orders
    {
        [Key]
        public int OrderID { get; set; }

        public int? OrderBookAmount { get; set; }

        public double? OrderTotalSum { get; set; }

        public int? Order_BookID { get; set; }

        public int? Order_UserID { get; set; }

        public string OrderDateStart { get; set; }

        public string OrderDateEnd { get; set; }

        public virtual Users Users { get; set; }
    }
}
