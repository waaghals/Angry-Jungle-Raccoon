using BarometerDomain;
using BarometerDomain.Model;
using BarometerDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BarometerStudent.Services
{
    public class ProjectService
    {
        Context context;
        public Group ByStudentAndProject(int studentId, int ProjectId)
        {
            Project project = GetProject(ProjectId);
            StudentRepository sr = new StudentRepository(context);
            Student student = sr.Get(studentId);

            foreach (Group groep in project.Groups)
            {
                if (groep.Student.Contains(student))
                    return groep;
            }
            return null;
        }

        public IEnumerable<String> GenerateAnnouncements(int id)
        {
            Project project = GetProject(id);
            LinkedList<String> ret = new LinkedList<String>();

            foreach (ProjectPeriod pp in project.ProjectPeriod)
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

        public Project GetProject(int id)
        {
            context = new Context();
            ProjectRepository pr = new ProjectRepository(context);
            return pr.Get(id);
        }

    }
}