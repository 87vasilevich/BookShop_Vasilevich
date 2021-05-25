using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork_BookShop.MVVM.Model
{
    class SQLNotificationRepository : IRepository<Notifications>
    {
        private ApplicationContext db;

        public SQLNotificationRepository()
        {
            this.db = new ApplicationContext();
        }
        public IEnumerable<Notifications> GetDataList()
        {
            return db.Notifications;
        }

        public Notifications GetElement(int ID)
        {
            return db.Notifications.Find(ID);
        }

        public void Create(Notifications item)
        {
            db.Notifications.Add(item);
        }

        public void Update(Notifications item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Notifications _notification = db.Notifications.Find(id);
            if (_notification != null)
            {
                db.Notifications.Remove(_notification);
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
