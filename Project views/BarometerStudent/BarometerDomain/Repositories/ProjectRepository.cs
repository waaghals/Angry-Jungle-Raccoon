using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarometerDomain.Model;

namespace BarometerDomain.Repositories
{
    public class ProjectRepository : GenericRepository<Project>
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
            return WithStudent(student.Id);
        }
        public IEnumerable<Project> WithStudent(int studentId)
        {
            Student s = new StudentRepository(database).Get(studentId);
            return s.Project.ToList();
        }

        public Group ByStudentAndProject(int studentId, int ProjectId)
        {
            Project proj = this.Get(ProjectId);
            Student student = new StudentRepository(database).Get(studentId);
            foreach (Group g in proj.Groups)
            {
                if (g.Student.Contains(student))
                    return g;
            }
            return null;
        }
    }
}
