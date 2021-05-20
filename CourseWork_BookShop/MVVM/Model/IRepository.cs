using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseWork_BookShop.MVVM.Model;

namespace CourseWork_BookShop.MVVM.Model
{
    interface IRepository<T> : IDisposable
    {
        IEnumerable<T> GetDataList();
        T GetElement(int ID);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        void Save();
    }
}
