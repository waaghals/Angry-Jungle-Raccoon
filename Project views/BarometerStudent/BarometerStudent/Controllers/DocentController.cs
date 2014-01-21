using BarometerDomain;
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

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MentorStudenten()
        {
            UserRepository userrep = new UserRepository(new Context());
            User user = userrep.Get(userID);
            List<Student> studenten = user.MentorStudent.ToList<Student>();
            return View(studenten);
        }

        [HttpGet]
        public ActionResult SelecteerProject()
        {
            ProjectRepository pr = new ProjectRepository(new Context());
            SelectList sl = new SelectList(pr.ByTutor(/*tutorid*/1), "Id", "Name");
            ViewBag.Project = sl;
            return View();
        }

        [HttpPost]
        public ActionResult SelecteerProject(Project project)
        {
            if (ModelState.IsValid)
            {
                TempData["myProject"] = project;
                return RedirectToAction("SelecteerTutorGroep", "Docent");
            }
            return View();
        }

        [HttpGet]
        public ActionResult SelecteerTutorGroep()
        {
            if (TempData.ContainsKey("myProject"))
            {
                Project myProject = (Project)TempData["myProject"];
                return View(myProject);
            }
            else
            {
                return RedirectToAction("SelecteerProject", "Docent");
            }
        }

        [HttpPost]
        public ActionResult SelecteerTutorgroep(Group group)
        {
            return View();
        }

        public ActionResult ProjectAanmaken()
        {
            ProjectRepository pr = new ProjectRepository(new Context());
            SelectList projecten = new SelectList(pr.GetAll(),"Id", "Name");
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
            project.Skill.Add(skill);
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
