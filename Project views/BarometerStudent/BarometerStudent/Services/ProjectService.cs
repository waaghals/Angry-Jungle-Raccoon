using BarometerDomain;
using BarometerDomain.Model;
using BarometerDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BarometerStudent.Services
{
    public class ProjectService
    {
        Context context;
        public ProjectService()
        {
            context = new Context();
        }
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
            ProjectRepository pr = new ProjectRepository(context);
            return pr.Get(id);
        }

        public SelectList GetTutorProject(int id)
        {
            ProjectRepository pr = new ProjectRepository(new Context());

            return new SelectList(pr.ByTutor(/*tutorid*/id), "Id", "Name", "1");
        }

        public SelectList GetTutorGroup(int projectId, int userId)
        {
            Project myProject = GetProject(projectId);
            IList<Group> tutorGroupList = new List<Group>();

            foreach (Group group in myProject.Groups)
            {
                if (group.Tutor.Id == userId)
                {
                    tutorGroupList.Add(group);
                }
            }
            return new SelectList(tutorGroupList, "Id", "Name");
        }


        public Project GetProjectFromGroup(int groep)
        {
            GroupRepository gr = new GroupRepository(context);
            int id = gr.Get(groep).Project.Id;
            return GetProject(id);
        }

        public void DeleteGroup(int groupId, int projectId)
        {
            GroupRepository gr = new GroupRepository(context);
            Group g = gr.Get(groupId);    
            if (g.Project.Id == projectId)
                {
                    g.Project = null;
                }

            gr.Update(g);
            gr.Save();
        }

        public void AddGroup(int groupId, int projectId)
        {
            GroupRepository gr = new GroupRepository(context);
            ProjectRepository pr = new ProjectRepository(context);
            Group g = gr.Get(groupId);
            Project p = pr.Get(projectId);
            p.Groups.Add(g);
            pr.Update(p);
            pr.Save();
        }

        public SelectList WithStudent(int studentId)
        {
            ProjectRepository pr = new ProjectRepository(context);
            return new SelectList(pr.WithStudent(studentId), "Id", "Name");
        }
    }
}