using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarometerDomain.Repositories
{
    class SkillRepository : GenericRepository<Skill>
    {
        public SkillRepository(Context c) : base(c) { }
    }
}
