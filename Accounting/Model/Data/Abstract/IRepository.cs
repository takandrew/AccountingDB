using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Model.Data.Abstract
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetFiltered(string selectedStatus, string selectedType, string selectedName);
        T GetByID(int id);
        void Save(T obj);
        void Delete(int id);
    }
}
