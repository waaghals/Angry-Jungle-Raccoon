using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using  System.Data.Entity.ModelConfiguration.Conventions;

namespace BarometerDomain.Repositories
{
    class GenericRepository<T> : IRepository<T>
    {
        Context database;
        public GenericRepository(Context context)
        {
            database = context;
        }

        public bool Delete(T entity)
        {
            
            return false;
        }

        public bool Delete(int id)
        {
            return false;
        }
        
        public T Get(int id)
        {
            return default(T);
        }

        public IEnumerable<T> GetAll()
        {
            return null;
        }

        public void Insert(T entity)
        {

        }

        public void Save()
        {

        }

        public bool Update(T entity)
        {
            return false;
        }

        private IEnumerable<IEntity> GetTable(T entity)
        {
            if (entity is Evaluation)
                return database.Evaluations;
            else if (entity is Group)
                return database.Groups;
            else if (entity is ProjectPeriod)
                return database.ProjectPeriods;
            else if (entity is Project)
                return database.Projects;
            else if (entity is Skill)
                return database.Skills;
            else if (entity is Student)
                return database.Students;
            else if (entity is User)
                return database.Users;
            return null;
        }
    }
}
