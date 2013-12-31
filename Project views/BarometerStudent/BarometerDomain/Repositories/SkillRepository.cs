using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarometerDomain.Model;

namespace BarometerDomain.Repositories
{
    public class SkillRepository : GenericRepository<Skill>
    {
        public SkillRepository(Context c)
            : base(c)
        {
            table = database.Skills;
        }
    }
}
