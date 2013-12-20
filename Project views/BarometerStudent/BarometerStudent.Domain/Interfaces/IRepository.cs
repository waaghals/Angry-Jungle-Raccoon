using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarometerStudent.Domain.Interfaces
{
    public interface IRepository<T>
    {

        Boolean Delete(T entity);
        Boolean Delete(int id);
        object Get(int id);
        IEnumerable<T> GetAll();
        void Insert(T entity);
        void Save();
        Boolean Update(T entity);

    }
}
