namespace CourseWork_BookShop.MVVM.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Admins
    {
        [Key]
        public int AdminID { get; set; }

        [StringLength(30)]
        public string AdminLogin { get; set; }

        public string AdminPassword { get; set; }
    }
}
