namespace CourseWork_BookShop.MVVM.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            Bank_Cards = new HashSet<Bank_Cards>();
            Basket = new HashSet<Basket>();
            Orders = new HashSet<Orders>();
        }

        [Key]
        public int UserID { get; set; }

        [StringLength(20)]
        public string UserName { get; set; }

        [StringLength(30)]
        public string UserSurname { get; set; }

        [StringLength(30)]
        public string UserOtchestvo { get; set; }

        [StringLength(20)]
        public string UserLogin { get; set; }

        public string UserPassword { get; set; }

        public string UserEmail { get; set; }

        [StringLength(20)]
        public string UserCity { get; set; }

        [StringLength(50)]
        public string UserStreet { get; set; }

        public int? UserHouse_number { get; set; }

        public int? UserApartament_number { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bank_Cards> Bank_Cards { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Basket> Basket { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
