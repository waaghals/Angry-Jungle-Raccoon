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

        public User ByLogin(string login)
        {
            User user = null;
            foreach (User u in table)
            {
                if (u.Login == login)
                {
                    user = u;
                    break;
                }
            }
            return user;
        }
    }
}
