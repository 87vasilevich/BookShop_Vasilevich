using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace CourseWork_BookShop.MVVM.Model
{
    public partial class ApplicationContext : DbContext
    {
        public ApplicationContext()
            : base("name=ApplicationContext")
        {
        }

        public virtual DbSet<Basket> Basket { get; set; }
        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<Notifications> Notifications { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
                .HasMany(e => e.Basket)
                .WithOptional(e => e.Users)
                .HasForeignKey(e => e.Basket_UserID);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.Users)
                .HasForeignKey(e => e.Order_UserID);
        }
    }
}
