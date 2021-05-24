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

        public string A_Name { get; set; }

        public string A_Surname { get; set; }

        public string B_Name { get; set; }

        public int? Order_BookID { get; set; }

        public int? Order_UserID { get; set; }

        public string OrderDateStart { get; set; }

        public string OrderDateEnd { get; set; }

        public virtual Users Users { get; set; }

        public Orders()
        {

        }

        public Orders(string _aname, string _asurname, string _bname, string _datestart, string _dateend, int _userid, int _bookid, double _totalsum, int _bookamunt)
        {
            this.A_Name = _aname;
            this.A_Surname = _asurname;
            this.B_Name = _bname;
            this.OrderDateStart = _datestart;
            this.OrderDateEnd = _dateend;
            this.Order_UserID = _userid;
            this.Order_BookID = _bookid;
            this.OrderTotalSum = _totalsum;
            this.OrderBookAmount = _bookamunt;
        }
    }
}
