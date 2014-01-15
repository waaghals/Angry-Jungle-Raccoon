using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarometerDomain.Model;

namespace BarometerDomain.Repositories
{
    public class StudentRepository : GenericRepository<Student>
    {
        public StudentRepository(Context c)
            : base(c)
        {
            table = database.Students;
        }

        public Student ByNumber(int number)
        {
            Student student = null;
            foreach(Student s in table)
                if (s.Number == number)
                {
                    student = s;
                    break;
                }
            return student;
        }
    }
}
