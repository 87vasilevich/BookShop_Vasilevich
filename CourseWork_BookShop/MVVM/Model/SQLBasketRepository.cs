using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork_BookShop.MVVM.Model
{
    class SQLBasketRepository : IRepository<Basket>
    {
        private ApplicationContext db;

        public SQLBasketRepository()
        {
            this.db = new ApplicationContext();
        }
        public IEnumerable<Basket> GetDataList()
        {
            return db.Basket;
        }

        public Basket GetElement(int ID)
        {
            return db.Basket.Find(ID);
        }

        public void Create(Basket item)
        {
            db.Basket.Add(item);
        }

        public void Update(Basket item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Basket basket = db.Basket.Find(id);
            if (basket != null)
            {
                db.Basket.Remove(basket);
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
