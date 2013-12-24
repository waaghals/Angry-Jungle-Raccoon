using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarometerDomain.Repositories
{
    public class ProjectRepository : GenericRepository<Project>
    {
        public ProjectRepository(Context c)
            : base(c)
        {
            table = database.Projects;
        }
    }
}
