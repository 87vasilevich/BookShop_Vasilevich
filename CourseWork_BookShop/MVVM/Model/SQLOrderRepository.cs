using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork_BookShop.MVVM.Model
{
    class SQLOrderRepository : IRepository<Orders>
    {
        private ApplicationContext db;

        public SQLOrderRepository()
        {
            this.db = new ApplicationContext();
        }
        public IEnumerable<Orders> GetDataList()
        {
            return db.Orders;
        }

        public Orders GetElement(int ID)
        {
            return db.Orders.Find(ID);
        }

        public void Create(Orders item)
        {
            db.Orders.Add(item);
        }

        public void Update(Orders item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Orders order = db.Orders.Find(id);
            if (order != null)
            {
                db.Orders.Remove(order);
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
