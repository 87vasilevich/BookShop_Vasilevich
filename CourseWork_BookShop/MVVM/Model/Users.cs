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
            Basket = new HashSet<Basket>();
            Orders = new HashSet<Orders>();
        }

        public Users(string _Password, string _Login)
        {
            Basket = new HashSet<Basket>();
            Orders = new HashSet<Orders>();

            this.UserPassword = _Password;
            this.UserLogin = _Login;
        }

        public Users(string _Password, string _Name, string _Surname, string _Otchestvo, string _City, string _Street, int _House, int _Apartament, string _Login, string _Email)
        {
            Basket = new HashSet<Basket>();
            Orders = new HashSet<Orders>();

            this.UserPassword = _Password;
            this.UserName = _Name;
            this.UserSurname = _Surname;
            this.UserOtchestvo = _Otchestvo;
            this.UserCity = _City;
            this.UserStreet = _Street;
            this.UserHouse_number = _House;
            this.UserApartament_number = _Apartament;
            this.UserLogin = _Login;
            this.UserEmail = _Email;
        }

        [Key]
        public int UserID { get; set; }

        public string UserName { get; set; }

        public string UserSurname { get; set; }

        public string UserOtchestvo { get; set; }

        [StringLength(30)]
        public string UserLogin { get; set; }

        public string UserPassword { get; set; }

        public string UserEmail { get; set; }

        public string UserCity { get; set; }

        public string UserStreet { get; set; }

        public int? UserHouse_number { get; set; }

        public int? UserApartament_number { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Basket> Basket { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
