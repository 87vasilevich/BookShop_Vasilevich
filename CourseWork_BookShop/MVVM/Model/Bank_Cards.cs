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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CardNumber { get; set; }

        public double? CardBalance { get; set; }

        public int? Card_UserID { get; set; }

        public virtual Users Users { get; set; }
    }
}
