using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarometerDomain.Model;

namespace BarometerDomain.Repositories
{
    class ProjectRepository : GenericRepository<Project>
    {
        public ProjectRepository(Context c)
            : base(c)
        {
            table = database.Projects;
        }

        public IEnumerable<Project> AllOpen()
        {
            List<Project> ret = new List<Project>();
            foreach (Project p in table)
                foreach (ProjectPeriod pPeriod in p.ProjectPeriod)
                    if (pPeriod.Start < DateTime.Now && pPeriod.End > DateTime.Now && !ret.Contains(p))
                        ret.Add(p);
            return ret;
        }

        public IEnumerable<Project> WithStudent(Student student)
        {
            List<Project> ret = new List<Project>();
            ret.AddRange(student.Project);
            return ret;
        }
        public IEnumerable<Project> WithStudent(int studentId)
        {
            List<Project> ret = new List<Project>();
            foreach(Student s in database.Students)
                if (s.Id == studentId)
                {
                    ret.AddRange(s.Project);
                    break;
                }
            return ret;
        }
    }
}
