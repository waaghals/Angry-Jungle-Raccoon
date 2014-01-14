using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarometerDomain.Model;

namespace BarometerDomain.Repositories
{
    public class ProjectPeriodRepository : GenericRepository<ProjectPeriod>
    {
        public ProjectPeriodRepository(Context c)
            : base(c)
        {
            table = database.ProjectPeriods;
        }

        public ICollection<Evaluation> GetEvaluations(int projectPeriodId, int byStudent, int forStudent)
        {
            List<Evaluation> ret = new List<Evaluation>();
            ProjectPeriod period = Get(projectPeriodId);

            foreach(Evaluation eval in period.Evaluation)
            {
                if (eval.By.Id == byStudent && eval.For.Id == forStudent)
                    ret.Add(eval);
            }
            return ret;
        }
    }
}
