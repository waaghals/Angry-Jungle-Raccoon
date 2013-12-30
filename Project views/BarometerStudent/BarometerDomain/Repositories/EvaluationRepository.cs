using BarometerDomain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarometerDomain.Repositories
{
    class EvaluationRepository : GenericRepository<Evaluation>
    {
        public EvaluationRepository(Context c)
            : base(c)
        {
            table = database.Evaluations;
        }

        public IEnumerable<Evaluation> ByStudent(Student student)
        {
            List<Evaluation> ret = new List<Evaluation>();
            foreach (Evaluation e in table)
                if (e.By == student)
                    ret.Add(e);
            return ret;
        }
        public IEnumerable<Evaluation> ByStudent(int studentId)
        {
            List<Evaluation> ret = new List<Evaluation>();
            foreach (Evaluation e in table)
                if (e.By.Id == studentId)
                    ret.Add(e);
            return ret;
        }

        public IEnumerable<Evaluation> ForStudent(Student student)
        {
            List<Evaluation> ret = new List<Evaluation>();
            foreach (Evaluation e in table)
                if (e.For == student)
                    ret.Add(e);
            return ret;
        }
        public IEnumerable<Evaluation> ForStudent(int studentId)
        {
            List<Evaluation> ret = new List<Evaluation>();
            foreach (Evaluation e in table)
                if (e.For.Id == studentId)
                    ret.Add(e);
            return ret;
        }
    }
}
