﻿using BarometerDomain;
using BarometerDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BarometerDomain.Model;

namespace BarometerStudent.Controllers
{
    public class DocentController : Controller
    {
        //
        // GET: /Docent/
        private int userID = 1;
        private int userProjectID = 1;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GroepToewijzenAanProject()
        {
            GroupRepository gr = new GroupRepository(new Context());
            ViewBag.addGroups = new MultiSelectList(gr.NotInProject(), "Id", "Name");
            ViewBag.deleteGroups = new MultiSelectList(gr.InProject(userProjectID), "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult DeleteGroup()
        {
            GroupRepository gr = new GroupRepository(new Context());
            string[] groups;
            groups = Request.Form["Groups"].Split(',');
            foreach (string group in groups)
            {
                Group g = gr.Get(Convert.ToInt32(group));
                if (g.Project.Id == userProjectID)
                {
                    g.Project = null;
                }
                gr.Update(g);
                gr.Save();
            }
            return RedirectToAction("GroepToewijzenAanProject");
        }


        public ActionResult MentorStudenten()
        {
            UserRepository userrep = new UserRepository(new Context());
            User user = userrep.Get(userID);
            List<Student> studenten = user.MentorStudent.ToList<Student>();
            return View(studenten);
        }

        public ActionResult SelecteerProject()
        {
            ProjectRepository pr = new ProjectRepository(new Context());

            ViewBag.selectListProjects = new SelectList(pr.ByTutor(/*tutorid*/userID), "Id", "Name", "1");
            return View();
        }

        [HttpPost]
        public ActionResult SelecteerProject(string project)
        {
            if (project != null)
            {
                int projectId = Convert.ToInt32(project);

                ProjectRepository pr = new ProjectRepository(new Context());
                Project myProject = pr.Get(projectId);

                TempData["myProject"] = myProject;

                return RedirectToAction("SelecteerTutorGroep", "Docent");
            }
            return View();
        }

        public ActionResult SelecteerTutorGroep()
        {
            if (TempData.ContainsKey("myProject"))
            {
                TempData.Keep();

                Project myProject = (Project)TempData["myProject"];
                IList<Group> tutorGroupList = new List<Group>();

                foreach (Group group in myProject.Groups)
                {
                    if (group.Tutor.Id == userID)
                    {
                        tutorGroupList.Add(group);
                    }
                }
                SelectList sl = new SelectList(tutorGroupList, "Id", "Name");

                ViewBag.selectListGroups = sl;
                return View();
            }
            else
            {
                return RedirectToAction("SelecteerProject", "Docent");
            }
        }

        [HttpPost]
        public ActionResult SelecteerTutorgroep(string group)
        {
            if (group != null)
            {
                int groupId = Convert.ToInt32(group);

                GroupRepository gr = new GroupRepository(new Context());
                Group myGroup = gr.Get(groupId);

                TempData["myGroup"] = myGroup;

                return RedirectToAction("ViewGroep", "Docent");
            }
            return View();
        }

        public ActionResult ViewGroep()
        {
            if (TempData.ContainsKey("myGroup") && TempData.ContainsKey("myProject"))
            {
                TempData.Keep();

                Group group = (Group)TempData["myGroup"];
                Project project = (Project)TempData["myProject"];

                List<ProjectPeriod> projectPeriodList = new List<ProjectPeriod>();

                foreach (ProjectPeriod period in group.Project.ProjectPeriod)
                {
                    if (group.Project.Id == project.Id)
                    {
                        projectPeriodList.Add(period);
                    }
                }
                List<Student> studentList = new List<Student>();
                Dictionary<string, List<Evaluation>> evalutionsPerForStudent = new Dictionary<string, List<Evaluation>>();

                foreach (Student student in group.Student)
                {
                    if (!(studentList.Contains(student)))
                    {
                        studentList.Add(student);
                        List<Evaluation> sortedList = BubbleSort((List<Evaluation>)project.GetEvaluations(student));
                        evalutionsPerForStudent.Add(student.Name, sortedList);
                    }
                }

                ViewBag.studenten = studentList;
                ViewBag.project = project;
                ViewBag.periodsCount = project.ProjectPeriod.Count;
                ViewBag.group = group;
                return View(evalutionsPerForStudent);
            }
            else
            {
                return RedirectToAction("SelecteerProject", "Docent");
            }
        }

        private List<Evaluation> BubbleSort(List<Evaluation> evaluationList)
        {
            for (int outer = evaluationList.Count - 1; outer >= 1; outer--)
                for (int inner = 0; inner < outer; inner++) // inner loop (forward)
                    if ((evaluationList[inner].CompareToWithPeriod(evaluationList[inner + 1]) == -1))
                    {
                        Evaluation temp = evaluationList[inner];
                        evaluationList[inner] = evaluationList[inner + 1];
                        evaluationList[inner + 1] = temp;
                    }
            return evaluationList;
        }

        public ActionResult ProjectAanmaken()
        {
            ProjectRepository pr = new ProjectRepository(new Context());
            SelectList projecten = new SelectList(pr.GetAll(), "Id", "Name");
            ViewBag.projecten = projecten;
            return View(new Project());
        }

        public ActionResult CompetentiesToevoegenAanProject(Project p)
        {
            if (Session["newProject"] == null)
            {
                Session["newProject"] = p;
            }
            else
            {
                p = (Project)Session["newProject"];
            }

            int baseProjectId = Convert.ToInt32(Session["baseProject"]);
            if (baseProjectId == 0)
            {
                Session["baseProject"] = Convert.ToInt32(Request.Form["Project"]);
                baseProjectId = (int)Session["baseProject"];
            }

            ProjectRepository pr = new ProjectRepository(new Context());
            MultiSelectList skillsProject = new MultiSelectList(p.Skill.ToList(), "Id", "Category");
            ViewBag.CompetentiesProject = skillsProject;
            List<Skill> skillLijst = pr.Get(baseProjectId).Skill.ToList();
            System.Diagnostics.Debug.WriteLine(skillLijst.Count);
            foreach (Skill s in p.Skill)
            {
                if (skillLijst.Contains(s))
                {
                    System.Diagnostics.Debug.WriteLine("Skill " + s.Category);
                    skillLijst.Remove(s);
                }
            }
            MultiSelectList skillsVoorgaandProject = new MultiSelectList(skillLijst, "Id", "Category");

            ViewBag.CompetentiesVoorgaandProject = skillsVoorgaandProject;
            return View();
        }

        [HttpPost]
        public ActionResult CompetentiesToevoegenAanLijst()
        {
            Project project = (Project)Session["newProject"];
            SkillRepository skillRepo = new SkillRepository(new Context());

            string IdsVoorgaandProject = Request.Form["competentiesVoorgaandProject"];
            string IdsProject = Request.Form["competentiesProject"];


            if (IdsVoorgaandProject != null)
            {
                string[] IdArrayVoorgaandProject = IdsVoorgaandProject.Split(',');
                foreach (string IdString in IdArrayVoorgaandProject)
                {
                    project.Skill.Add(skillRepo.Get(Convert.ToInt32(IdString)));
                }
            }

            if (IdsProject != null)
            {
                string[] IdArrayProject = IdsProject.Split(',');
                foreach (string IdString in IdArrayProject)
                {
                    project.Skill.Remove(skillRepo.Get(Convert.ToInt32(IdString)));
                }
            }
            Session["newProject"] = project;
            return RedirectToAction("CompetentiesToevoegenAanProject", (Project)null);
        }

        public ActionResult BeoordelingsmomentenToevoegen()
        {
            Project project = (Project)Session["newProject"];
            List<ProjectPeriod> projectPeriods = project.ProjectPeriod.ToList();
            ViewBag.Beoordelingsmomenten = projectPeriods;
            return View();
        }

        [HttpPost]
        public ActionResult BeoordelingsmomentToevoegen(ProjectPeriod p)
        {
            Project project = (Project)Session["newProject"];
            project.ProjectPeriod.Add(new ProjectPeriod() { Name = p.Name, Start = p.Start, End = p.End });
            Session["newProject"] = project;
            return RedirectToAction("BeoordelingsmomentenToevoegen", "Docent");
        }

        public ActionResult ProjectToevoegen()
        {
            Project project = (Project)Session["newProject"];
            Session["newProject"] = null;
            return RedirectToAction("ProjectAanmaken", "Docent");
        }

    }
}
