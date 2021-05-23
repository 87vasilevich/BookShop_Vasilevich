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

        public int? BasketBookAmount { get; set; }

        public virtual Users Users { get; set; }

        public Basket()
        {

        }

        public Basket(int _userID, int _bookID, int _bookamount)
        {
            this.Basket_UserID = _userID;
            this.Basket_BookID = _bookID;
            this.BasketBookAmount = _bookamount;
        }
    }
}
