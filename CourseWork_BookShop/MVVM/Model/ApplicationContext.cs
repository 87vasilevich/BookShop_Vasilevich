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

        public virtual DbSet<Admins> Admins { get; set; }
        public virtual DbSet<Bank_Cards> Bank_Cards { get; set; }
        public virtual DbSet<Basket> Basket { get; set; }
        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
                .HasMany(e => e.Bank_Cards)
                .WithOptional(e => e.Users)
                .HasForeignKey(e => e.Card_UserID);

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
