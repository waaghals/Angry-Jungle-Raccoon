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
            StudentRepository studentrepository = new StudentRepository(context);
            Student student = studentrepository.Get(studentId);

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

            foreach (ProjectPeriod projectperiod in project.ProjectPeriod)
            {
                if (DateTime.Now < projectperiod.End && DateTime.Now > projectperiod.Start)
                {
                    ret.AddFirst(projectperiod.Start.ToShortDateString() + " - " + projectperiod.Name + " staat op dit moment open tot " + projectperiod.End.ToShortDateString());
                }       
            }

            if(ret.Count == 0)
            {
                ret.AddFirst("Er staan op dit moment geen beoordelingsmomenten open.");
            }

            return ret;
        }

        public Project GetProject(int id)
        {
            ProjectRepository projectrepository = new ProjectRepository(context);
            return projectrepository.Get(id);
        }

        public SelectList GetTutorProject(int id)
        {
            ProjectRepository projectrepository = new ProjectRepository(new Context());

            return new SelectList(projectrepository.ByTutor(/*tutorid*/id), "Id", "Name");
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
            GroupRepository grouprepository = new GroupRepository(context);
            int id = grouprepository.Get(groep).Project.Id;
            return GetProject(id);
        }

        public void DeleteGroup(int groupId, int projectId)
        {
            GroupRepository grouprepository = new GroupRepository(context);
            Group group = grouprepository.Get(groupId);
            if (group.Project.Id == projectId)
            {
                group.Project = null;
            }

            grouprepository.Update(group);
            grouprepository.Save();
        }

        public void AddGroup(int groupId, int projectId)
        {
            GroupRepository grouprepository = new GroupRepository(context);
            ProjectRepository projectrepository = new ProjectRepository(context);
            EvaluationRepository evaluationrepository = new EvaluationRepository(context);
            Group group = grouprepository.Get(groupId);
            Project project = projectrepository.Get(projectId);
            bool createEvals = true;
            foreach (Evaluation evaluation in project.ProjectPeriod.First().Evaluation)
            {
                foreach (Student byStudent in group.Student)
                {
                    foreach (ProjectPeriod projectPeriod in project.ProjectPeriod)
                    {
                        foreach (Skill skill in project.Skill)
                        {
                            foreach (Student forStudent in group.Student)
                            {
                                if (evaluation.For.Id == forStudent.Id && evaluation.By.Id == byStudent.Id && evaluation.ProjectPeriod.Id == projectPeriod.Id && evaluation.Skill.Id == skill.Id)
                                {
                                    createEvals = false;
                                }
                            }
                        }
                    }
                }
            }
            if (createEvals)
            {
                foreach (Student byStudent in group.Student)
                {
                    foreach (ProjectPeriod projectPeriod in project.ProjectPeriod)
                    {
                        foreach (Skill skill in project.Skill)
                        {
                            foreach (Student forStudent in group.Student)
                            {
                                if (byStudent.Id != forStudent.Id)
                                {
                                    Evaluation eval = new Evaluation() { For = forStudent, By = byStudent, ProjectPeriod = projectPeriod, Skill = skill };
                                    evaluationrepository.Insert(eval);
                                }
                            }
                        }
                    }
                }
            }
            project.Groups.Add(group);
            projectrepository.Update(project);
            projectrepository.Save();
        }

        public SelectList WithStudent(int studentId)
        {
            ProjectRepository projectrepository = new ProjectRepository(context);
            return new SelectList(projectrepository.WithStudent(studentId), "Id", "Name");
        }

        public Group GetGroupById(int groupId)
        {
            GroupRepository grouprepository = new GroupRepository(context);
            return grouprepository.Get(groupId);
        }
    }
}