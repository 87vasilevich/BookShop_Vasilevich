namespace CourseWork_BookShop.MVVM.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Bank_Cards
    {
        [Key]
        public int CardID { get; set; }

        public string CardNumber { get; set; }

        public double? CardBalance { get; set; }

        public int? Card_UserID { get; set; }

        public virtual Users Users { get; set; }

        public Bank_Cards(int _tempID, string _Card_number, float _Balance)
        {
            this.Card_UserID = _tempID;
            this.CardNumber = _Card_number;
            this.CardBalance = _Balance;
        }
    }
}
