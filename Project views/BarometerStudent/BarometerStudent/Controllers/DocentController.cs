using BarometerDomain;
using BarometerDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BarometerDomain.Model;
using BarometerStudent.Services;

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
            ProjectService ps = new ProjectService();
            ViewBag.selectListProjects = ps.GetTutorProject(/*tutorid*/userID);
            return View();
        }

        [HttpPost]
        public ActionResult SelecteerTutorGroep(int project)
        {
            ProjectService ps = new ProjectService();
            ViewBag.selectListGroups = ps.GetTutorGroup(project, userID);
            return View();
        }

        [HttpPost]
        public ActionResult ViewGroep(int groep)
        {
            ProjectService ps = new ProjectService();
            EvaluationService es = new EvaluationService();
            Project project = ps.GetProjectFromGroup(groep);
            ViewBag.project = project;
            ViewBag.periodsCount = project.ProjectPeriod.Count;
            return View(es.GetGroupEvaluation(groep));
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

        public ActionResult CompetentiesToevoegen(Skill skill)
        {
            Project project = (Project)Session["newProject"];
            SkillRepository skillRepo = new SkillRepository(new Context());
            Skill s = skillRepo.SkillExists(skill.Category);
            if (s == null)
            {
                project.Skill.Add(skill);
                System.Diagnostics.Debug.WriteLine("Nieuwe skill toegevoegd: " + skill.Category);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Bestaande skill toegevoegd: " + s.Category);
                project.Skill.Add(s);
            }
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
