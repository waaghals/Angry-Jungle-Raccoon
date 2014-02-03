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

        public IEnumerable<Project> AllOpen(List<Project> projects)
        {
            List<Project> ret = new List<Project>();
            foreach (Project p in projects)
            {
                foreach (ProjectPeriod pp in p.ProjectPeriod)
                {
                    if (pp.End > DateTime.Now && !ret.Contains(p))
                    {
                        ret.Add(p);
                    }
                }
            }
            return ret;
        }

        public IEnumerable<Project> WithStudent(Student student)
        {
            return WithStudent(student.Id);
        }

        public IEnumerable<Project> WithStudent(int studentId)
        {
            Student student = new StudentRepository(database).Get(studentId);
            List<Project> templist = new List<Project>();
            foreach(Group group in student.Groups)
            {
                if (group.Project!=null)
                templist.Add(group.Project);
            }
            return templist;
        }

        public IEnumerable<Project> ByTutor(int tutor)
        {
            List<Project> projects = new List<Project>();
            foreach(Group group in database.Groups.ToList<Group>())
            {
                //if(group.Tutor.Id == tutor)
                {
                    if(!projects.Contains(group.Project)&&group.Project!=null)
                        projects.Add(group.Project);
                }
            }
            return projects;
        }

    }
}
