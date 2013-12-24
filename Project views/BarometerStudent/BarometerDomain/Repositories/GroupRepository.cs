using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarometerDomain.Repositories
{
    class GroupRepository : GenericRepository<Group>
    {
        public GroupRepository(Context c) : base(c) { }
    }
}
