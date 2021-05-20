using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork_BookShop.MVVM.Model
{
    class SQLCardRepository : IRepository<Bank_Cards>
    {
        private ApplicationContext db;

        public SQLCardRepository()
        {
            this.db = new ApplicationContext();
        }
        public IEnumerable<Bank_Cards> GetDataList()
        {
            return db.Bank_Cards;
        }

        public Bank_Cards GetElement(int ID)
        {
            return db.Bank_Cards.Find(ID);
        }

        public void Create(Bank_Cards item)
        {
            db.Bank_Cards.Add(item);
        }

        public void Update(Bank_Cards item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Bank_Cards card = db.Bank_Cards.Find(id);
            if (card != null)
            {
                db.Bank_Cards.Remove(card);
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
