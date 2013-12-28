﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarometerDomain.Model;

namespace BarometerDomain.Repositories
{
    class StudentRepository : GenericRepository<Student>
    {
        public StudentRepository(Context c)
            : base(c)
        {
            table = database.Students;
        }
    }
}
