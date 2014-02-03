﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BarometerDomain.Model;
using BarometerStudent.Services;
using System.IO;
using BarometerDomain;
using BarometerDomain.Repositories;

namespace BarometerStudent.Controllers
{
    public class DocentController : Controller
    {
        //
        // GET: /Docent/

        public ActionResult Index()
        {
            return View();
        }

        private int getSessionDocentId()
        {
            UserRepository userrep = new UserRepository(new Context());
            User user = userrep.Get(((User)HttpContext.Session["User"]).Id);
            return user.Id;
        }

        public ActionResult GroepsindelingAanmaken()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImporterenExelSheet(HttpPostedFileBase file)
        {
            System.Diagnostics.Debug.WriteLine("file uploaded: " + file);
            if (file != null)
            {
                //zoek bestand en sla het op
                string fileName = Path.GetFileName(file.FileName);
                string path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                file.SaveAs(path);
                try
                {
                    //zet het in de db
                    new Services.ExcelReader(path).ToDatabase();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
                //verwijder het
                System.IO.File.Delete(path);


            }
            return View("GroepsindelingAanmaken");
        }


        public ActionResult GroepToewijzenAanProject()
        {
            GroupRepository gr = new GroupRepository(new Context());
            ViewBag.addGroups = new MultiSelectList(gr.NotInProject(), "Id", "Name");
            ViewBag.deleteGroups = new MultiSelectList(gr.InProject((Project)Session["newProject"]), "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult DeleteGroup()
        {
            string varGroups = Request.Form["Groups"];
            List<string> groups = new List<string>();

            if (varGroups == null)
            {
                return RedirectToAction("GroepToewijzenAanProject");
            }
            else if (varGroups.Length < 2)
            {
                groups.Add(varGroups);
            }
            else
            {
                groups = varGroups.Split(',').ToList(); ;
            }

            ProjectService ps = new ProjectService();

            foreach (string group in groups)
            {
                ps.DeleteGroup(Convert.ToInt32(group), ps.GetProjectFromGroup(Convert.ToInt32(group)).Id);
            }

            return RedirectToAction("GroepToewijzenAanProject");

        }

        [HttpPost]
        public ActionResult AddGroup()
        {
            string varGroups = Request.Form["Groups"];
            List<string> groups = new List<string>();

            if (varGroups == null)
            {
                return RedirectToAction("GroepToewijzenAanProject");
            }
            else if (varGroups.Length < 2)
            {
                groups.Add(varGroups);
            }
            else
            {
                groups = varGroups.Split(',').ToList(); ;
            }

            ProjectService ps = new ProjectService();

            foreach (string group in groups)
            {
                ps.AddGroup(Convert.ToInt32(group), ps.GetProjectFromGroup(Convert.ToInt32(group)).Id);
            }
            return RedirectToAction("GroepToewijzenAanProject");

        }

        public ActionResult ProjectAanmakenRedirect()
        {
            Session["ProjectContext"] = new Context();
            Session["newProject"] = null;
            return RedirectToAction("ProjectAanmaken");
        }

        public ActionResult MentorStudenten()
        {
            StudentService sr = new StudentService();
            List<Student> studenten = sr.GetMentorStudents(getSessionDocentId());
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

        public ActionResult ProjectWijzigen()
        {
            if (Session["ProjectContext"] == null)
            {
                Session["ProjectContext"] = new Context();
            }

            ProjectRepository pr = new ProjectRepository((Context)Session["ProjectContext"]);
            List<Project> projectList = pr.GetAll().ToList();

            SelectList projecten = new SelectList(pr.AllOpen(projectList), "Id", "Name");
            ViewBag.projecten = projecten;
            return View();
        }
        public ActionResult GeselecteerdProjectWijzigen()
        {
            ProjectRepository pr = new ProjectRepository((Context)Session["ProjectContext"]);
            Session["newProject"] = pr.Get(Convert.ToInt32(Request.Form["Project"]));
            return RedirectToAction("ProjectAanmaken");
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
            else if (Request.Form["toewijzen"] != null)
            {
                return RedirectToAction("GroepToewijzenAanProject");
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
                if ((s.Category.ToLower().Equals(sk.Category.ToLower())) || (s.Id == sk.Id) && s.Id != 0)
                {
                    canAdd = false;
                }
            }
            if (canAdd)
            {
                Context con = (Context)Session["ProjectContext"];
                SkillRepository skillrepo = new SkillRepository(con);
                if(!skillrepo.GetAll().Contains(s))
                {
                    skillrepo.Insert(s); 
                    skillrepo.Save();
                }
                project.Skill.Add(s);
            }

            return RedirectToAction("CompetentiesToevoegenAanProject", (Project)null);
        }

        public ActionResult BeoordelingsmomentVerwijderen(int id)
        {
            System.Diagnostics.Debug.WriteLine(id);
            ProjectPeriodRepository pprepo = new ProjectPeriodRepository((Context)Session["ProjectContext"]);
            ProjectPeriod pp = pprepo.Get(id);
            Project proj = (Project)Session["newProject"];
            proj.ProjectPeriod.Remove(pp);
            pprepo.Delete(id);
            pprepo.Save();
            return RedirectToAction("BeoordelingsmomentenToevoegen", "Docent");
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
            ProjectPeriod projperiod = new ProjectPeriod() { Name = p.Name, Start = p.Start, End = p.End };
            Context con = (Context)Session["ProjectContext"];
            ProjectPeriodRepository projperiodrepo = new ProjectPeriodRepository(con);
            projperiodrepo.Insert(projperiod);
            projperiodrepo.Save();
            project.ProjectPeriod.Add(projperiod);
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
            ViewBag.selectListProjects = ps.GetTutorProject(/*tutorid*/getSessionDocentId());
            return View();
        }

        [HttpPost]
        public ActionResult SelecteerTutorGroep(int project)
        {
            ProjectService ps = new ProjectService();
            ViewBag.selectListGroups = ps.GetTutorGroup(project, getSessionDocentId());
            return View();
        }

        [HttpPost]
        public ActionResult ViewGroep(int groep)
        {
            ProjectService ps = new ProjectService();
            EvaluationService es = new EvaluationService();
            Project project = ps.GetProjectFromGroup(groep);

            ViewBag.groupName = ps.GetGroupById(groep).Name;
            ViewBag.project = project;
            ViewBag.periodsCount = project.ProjectPeriod.Count;
            return View(es.GetGroupEvaluation(groep));
        }

    }
}
