﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarometerDomain.Repositories
{
    class UserRepository : GenericRepository<User>
    {
        public UserRepository(Context c) : base(c) { }
    }
}
