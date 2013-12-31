using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarometerDomain.Model;

namespace BarometerDomain.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(Context c)
            : base(c)
        {
            table = database.Users;
        }
    }
}
