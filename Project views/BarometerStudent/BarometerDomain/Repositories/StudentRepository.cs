using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarometerDomain.Repositories
{
    public class StudentRepository : GenericRepository<Student>
    {
        public StudentRepository(Context c)
            : base(c)
        {
            table = database.Students;
        }
    }
}
