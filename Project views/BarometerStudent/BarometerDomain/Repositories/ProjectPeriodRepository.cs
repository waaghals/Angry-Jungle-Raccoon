using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarometerDomain.Model;

namespace BarometerDomain.Repositories
{
    class ProjectPeriodRepository : GenericRepository<ProjectPeriod>
    {
        public ProjectPeriodRepository(Context c)
            : base(c)
        {
            table = database.ProjectPeriods;
        }
    }
}
