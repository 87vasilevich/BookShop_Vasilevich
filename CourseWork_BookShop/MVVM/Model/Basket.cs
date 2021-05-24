namespace CourseWork_BookShop.MVVM.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Basket")]
    public partial class Basket
    {
        public int BasketID { get; set; }

        public int? Basket_UserID { get; set; }

        public int? Basket_BookID { get; set; }

        public string A_Name { get; set; }

        public string A_Surname { get; set; }

        public string B_Name { get; set; }

        public double? BasketTotalSum { get; set; }

        public int? BasketBookAmount { get; set; }

        public virtual Users Users { get; set; }

        public Basket()
        {

        }

        public Basket(int _userID, int _bookID, int _bookamount, double _baskettotalsum, string _aname, string _asurname, string _bname)
        {
            this.Basket_UserID = _userID;
            this.Basket_BookID = _bookID;
            this.BasketBookAmount = _bookamount;
            this.BasketTotalSum = _baskettotalsum;
            this.A_Name = _aname;
            this.A_Surname = _asurname;
            this.B_Name = _bname;
        }
    }
}
