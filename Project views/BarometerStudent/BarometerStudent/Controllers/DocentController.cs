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

        public ActionResult ProjectAanmakenRedirect()
        {
            Session["ProjectContext"] = new Context();
            Session["newProject"] = null;
            return RedirectToAction("ProjectAanmaken");
        }

        public ActionResult MentorStudenten()
        {
            UserRepository userrep = new UserRepository(new Context());
            User user = userrep.Get(userID);
            List<Student> studenten = user.MentorStudent.ToList<Student>();
            return View(studenten);
        }

        public ActionResult ProjectAanmaken()
        {
            ProjectRepository pr = new ProjectRepository((Context)Session["ProjectContext"]);
            SelectList projecten = new SelectList(pr.GetAll(), "Id", "Name");
            ViewBag.projecten = projecten;
            Project sessionProject = (Project)Session["newProject"];
            return View(sessionProject);
        }

        public ActionResult CompetentiesToevoegenAanProject()
        {
            Project p = (Project)Session["newProject"];

            int baseProjectId = Convert.ToInt32(Session["baseProject"]);
            if (baseProjectId == 0)
            {
                Session["baseProject"] = Convert.ToInt32(Request.Form["Project"]);
                baseProjectId = (int)Session["baseProject"];
            }

            ProjectRepository pr = new ProjectRepository((Context)Session["ProjectContext"]);
            MultiSelectList skillsProject = new MultiSelectList(p.Skill.ToList(), "Id", "Category");
            ViewBag.CompetentiesProject = skillsProject;

            List<Skill> skillLijst = pr.Get(baseProjectId).Skill.ToList();
            foreach (Skill s in p.Skill)
            {
                if (skillLijst.Contains(s))
                {
                    skillLijst.Remove(s);
                }
            }

            MultiSelectList skillsVoorgaandProject = new MultiSelectList(skillLijst, "Id", "Category");
            ViewBag.CompetentiesVoorgaandProject = skillsVoorgaandProject;
            return View();
        }

        public ActionResult ProjectOpslaan(Project p)
        {
            if (Session["newProject"] == null)
            {
                Session["newProject"] = p;
            }
            Session["baseProject"] = Request.Form["Project"];

            if (Request.Form["competenties"] != null)
            {
                return RedirectToAction("CompetentiesToevoegenAanProject");
            }
            else if (Request.Form["beoordelingsmoment"] != null)
            {
                return RedirectToAction("BeoordelingsmomentenToevoegen");
            }
            else if (Request.Form["bevestigen"] != null)
            {
                return RedirectToAction("ProjectToevoegen");
            }


            return RedirectToAction("ProjectAanmaken");
        }

        [HttpPost]
        public ActionResult CompetentiesToevoegenAanLijst()
        {
            Project project = (Project)Session["newProject"];
            SkillRepository skillRepo = new SkillRepository((Context)Session["ProjectContext"]);

            string IdsVoorgaandProject = Request.Form["competentiesVoorgaandProject"];
            string IdsProject = Request.Form["competenties"];

            if (IdsVoorgaandProject != null)
            {
                string[] IdArrayVoorgaandProject = IdsVoorgaandProject.Split(',');

                foreach (string IdString in IdArrayVoorgaandProject)
                {
                    Skill toevoegSkill = skillRepo.Get(Convert.ToInt32(IdString));
                    project.Skill.Add(toevoegSkill);
                }
            }

            if (IdsProject != null)
            {
                string[] IdArrayProject = IdsProject.Split(',');
                foreach (string IdString in IdArrayProject)
                {
                    Skill verwijder = skillRepo.Get(Convert.ToInt32(IdString));
                    if (project.Skill.Contains(verwijder))
                    {
                        project.Skill.Remove(verwijder);
                    }
                }
            }
            Session["newProject"] = project;
            return RedirectToAction("CompetentiesToevoegenAanProject", (Project)null);
        }

        public ActionResult CompetentiesToevoegen(Skill skill)
        {
            Project project = (Project)Session["newProject"];
            SkillRepository skillRepo = new SkillRepository((Context)Session["ProjectContext"]);
            Skill s = skillRepo.SkillExists(skill.Category);
            if (s == null)
            {
                s = skill;
            }

            bool canAdd = true;
            foreach (Skill sk in project.Skill)
            {
                if ((s.Category.ToLower().Equals(sk.Category.ToLower())) || (s.Id == sk.Id))
                {
                    canAdd = false;
                }
            }
            if (canAdd)
            {
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
            /*System.Diagnostics.Debug.WriteLine("Naam:" + project.Name);
            System.Diagnostics.Debug.WriteLine("Omschrijving:" + project.Description);
            System.Diagnostics.Debug.WriteLine("Anoniem:" + project.Anonymous);
            System.Diagnostics.Debug.WriteLine("Skills:");
            foreach (Skill s in project.Skill)
            {
                 System.Diagnostics.Debug.WriteLine(s.Category);
            }
            System.Diagnostics.Debug.WriteLine("Beoordelingsmomenten:");
            foreach (ProjectPeriod p in project.ProjectPeriod)
            {
                System.Diagnostics.Debug.WriteLine(p.Name + " " + p.Start + " " + p.End);
            }*/
            ProjectRepository projrepo = new ProjectRepository((Context)Session["ProjectContext"]);
            projrepo.Insert(project);
            projrepo.Save();
            Session["newProject"] = null;
            return RedirectToAction("Index", "Docent");
        }

        //public ActionResult GroepToewijzenAanProject()
        //{
        //    GroupRepository gr = new GroupRepository(new Context());
        //    ViewBag.addGroups = new MultiSelectList(gr.NotInProject(), "Id", "Name");
        //    ViewBag.deleteGroups = new MultiSelectList(gr.InProject(userProjectID), "Id", "Name");
        //    return View();
        //}
        //
        //[HttpPost]
        //public ActionResult DeleteGroup()
        //{
        //    GroupRepository gr = new GroupRepository(new Context());
        //    string[] groups;
        //    if (true)
        //    {
        //
        //    }
        //    groups = Request.Form["Groups"].Split(',');
        //    foreach (string group in groups)
        //    {
        //        Group g = gr.Get(Convert.ToInt32(group));
        //        if (g.Project.Id == userProjectID)
        //        {
        //            g.Project = null;
        //        }
        //        gr.Update(g);
        //        gr.Save();
        //    }
        //    return RedirectToAction("GroepToewijzenAanProject");
        //
        //}
        //
        //[HttpPost]
        //public ActionResult AddGroup()
        //{
        //    Context c = new Context();
        //    GroupRepository gr = new GroupRepository(c);
        //    ProjectRepository pr = new ProjectRepository(c);
        //    string[] groups = Request.Form["Groups"].Split(',');
        //    foreach (string group in groups)
        //    {
        //        Group g = gr.Get(Convert.ToInt32(group));
        //        Project p = pr.Get(userProjectID);
        //        p.Groups.Add(g);
        //        pr.Update(p);
        //        pr.Save();
        //    }
        //    return RedirectToAction("GroepToewijzenAanProject");
        //
        //}

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

    }
}
