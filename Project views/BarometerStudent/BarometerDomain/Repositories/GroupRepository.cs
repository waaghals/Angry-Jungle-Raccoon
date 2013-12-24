using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarometerDomain.Repositories
{
    public class GroupRepository : GenericRepository<Group>
    {
        public GroupRepository(Context c) 
            : base(c) 
        {
            table = database.Groups;
        }
    }
}
