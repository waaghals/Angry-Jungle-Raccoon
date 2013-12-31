using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BarometerDomain.Repositories
{
    public interface IRepository<T>
    {

        bool Delete(T entity);
        bool Delete(int id);
        T Get(int id);
        IEnumerable<T> GetAll();
        void Insert(T entity);
        void Save();
        bool Update(T entity);
    }
}
