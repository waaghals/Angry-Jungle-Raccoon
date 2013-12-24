using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarometerDomain.Repositories
{
    class EvaluationRepository : GenericRepository<Evaluation>
    {
        public EvaluationRepository(Context c) : base(c) { }
    }
}
