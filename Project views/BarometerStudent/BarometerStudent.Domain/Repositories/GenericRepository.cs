using BarometerStudent.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarometerStudent.Domain.Repositories
{
    abstract class GenericRepository<T> : IRepository<T>
    {

        public bool Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public object Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public bool Update(T entity)
        {
            throw new NotImplementedException();
        }

        bool IRepository<T>.Delete(T entity)
        {
            throw new NotImplementedException();
        }

        bool IRepository<T>.Delete(int id)
        {
            throw new NotImplementedException();
        }

        object IRepository<T>.Get(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<T> IRepository<T>.GetAll()
        {
            throw new NotImplementedException();
        }

        void IRepository<T>.Insert(T entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<T>.Save()
        {
            throw new NotImplementedException();
        }

        bool IRepository<T>.Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
