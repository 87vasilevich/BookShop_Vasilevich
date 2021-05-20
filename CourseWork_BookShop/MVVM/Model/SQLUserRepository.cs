using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork_BookShop.MVVM.Model
{
    class SQLUserRepository : IRepository<Users>
    {
        private ApplicationContext db;

        public SQLUserRepository()
        {
            this.db = new ApplicationContext();
        }
        public IEnumerable<Users> GetDataList()
        {
            return db.Users;
        }

        public Users GetElement(int ID)
        {
            return db.Users.Find(ID);
        }

        public void Create(Users item)
        {
            db.Users.Add(item);
        }

        public void Update(Users item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Users user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user);
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

