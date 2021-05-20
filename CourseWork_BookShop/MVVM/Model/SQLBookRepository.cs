using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork_BookShop.MVVM.Model
{
    class SQLBookRepository : IRepository<Books>
    {
        private ApplicationContext db;

        public SQLBookRepository()
        {
            this.db = new ApplicationContext();
        }
        public IEnumerable<Books> GetDataList()
        {
            return db.Books;
        }

        public Books GetElement(int ID)
        {
            return db.Books.Find(ID);
        }

        public void Create(Books item)
        {
            db.Books.Add(item);
        }

        public void Update(Books item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Books book = db.Books.Find(id);
            if (book != null)
            {
                db.Books.Remove(book);
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
