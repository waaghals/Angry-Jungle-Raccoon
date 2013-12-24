using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using  System.Data.Entity.ModelConfiguration.Conventions;

namespace BarometerDomain.Repositories
{
    class GenericRepository<T> : IRepository<T> where T : class, IEntity
    {
        protected Context database;
        protected DbSet<T> table;
        public GenericRepository(Context context)
        {
            database = context;
        }

        public bool Delete(T entity)
        {
            T deleteEntity = Get(entity.Id);
            if (deleteEntity != default(T))
            {
                table.Remove(deleteEntity);
                Save();
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            T entity = Get(id);
            if (entity != default(T))
            {
                table.Remove(entity);
                Save();
                return true;
            }
            return false;
        }
        
        public T Get(int id)
        {
            foreach (T entity in table)
                if (entity.Id == id)
                    return entity;
            return default(T);
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public void Insert(T entity)
        {
            table.Add(entity);
            Save();
        }

        public void Save()
        {
            database.SaveChanges();
        }

        public bool Update(T entity)
        {
            if (Delete(entity))
            {
                table.Add(entity);
                Save();
                return true;
            }
            return false;
        }

    }
}
