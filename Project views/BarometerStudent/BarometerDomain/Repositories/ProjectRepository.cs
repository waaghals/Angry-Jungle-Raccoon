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
            Student student = new StudentRepository(database).Get(studentId);
            List<Project> templist = new List<Project>();
            foreach(Group group in student.Groups)
            {
                templist.Add(group.Project);
            }
            return templist;
        }

        public IEnumerable<Project> ByTutor(int tutor)
        {
            List<Project> projects = new List<Project>();
            foreach(Group group in database.Groups.ToList<Group>())
            {
                if(group.Tutor.Id == tutor)
                {
                    if(!projects.Contains(group.Project))
                        projects.Add(group.Project);
                }
            }
            return projects;
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

        public IEnumerable<String> GenerateAnnouncements(int projectId)
        {
            LinkedList<String> ret = new LinkedList<String>();

            foreach (ProjectPeriod pp in Get(projectId).ProjectPeriod)
            {
                if (pp.Start.Date < DateTime.Now)
                {
                    ret.AddFirst(pp.Start.ToShortDateString() + " - Beoordelingsmoment " + pp.Name + " is nu geopend");
                    if (pp.End.Date < DateTime.Now)
                        ret.AddFirst(pp.End.ToShortDateString() + " - Beoordelingsmoment " + pp.Name + "Is nu gesloten");
                    else
                    {
                        TimeSpan ts = pp.End - DateTime.Now;
                        ret.AddFirst("Je hebt nog " + ts.Days + " dag(en) om beoordelingsmoment " + pp.Name + " in te vullen");
                    }
                }
            }

            return ret;
        }
    }
}
