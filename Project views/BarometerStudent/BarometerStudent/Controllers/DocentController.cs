using System;
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
            GroupRepository grouprepository = new GroupRepository(new Context());
            ViewBag.addGroups = new MultiSelectList(grouprepository.NotInProject(), "Id", "Name");
            ViewBag.deleteGroups = new MultiSelectList(grouprepository.InProject((Project)Session["newProject"]), "Id", "Name");
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
            Session["newProject"] = new Project();
            return RedirectToAction("ProjectAanmaken");
        }

        public ActionResult MentorStudenten()
        {
            StudentService studentservice = new StudentService();
            List<Student> studenten = studentservice.GetMentorStudents(getSessionDocentId());
            return View(studenten);
        }

        public ActionResult ProjectAanmaken()
        {
            ProjectRepository projectrepository = new ProjectRepository((Context)Session["ProjectContext"]);
            SelectList projecten = new SelectList(projectrepository.GetAll(), "Id", "Name");
            ViewBag.projecten = projecten;
            UserRepository userRepo = new UserRepository((Context)Session["ProjectContext"]);
            SelectList tutors = new SelectList(userRepo.GetTutors(), "Id", "Name");
            ViewBag.tutors = tutors;
            Project sessionProject = (Project)Session["newProject"];
            return View(sessionProject);
        }

        public ActionResult CompetentiesToevoegenAanProject()
        {
            Project project = (Project)Session["newProject"];

            int baseProjectId = Convert.ToInt32(Session["baseProject"]);
            if (baseProjectId == 0)
            {
                Session["baseProject"] = Convert.ToInt32(Request.Form["Project"]);
                baseProjectId = (int)Session["baseProject"];
            }

            ProjectRepository projectrepository = new ProjectRepository((Context)Session["ProjectContext"]);
            MultiSelectList skillsProject = new MultiSelectList(project.Skill.ToList(), "Id", "Category");
            ViewBag.CompetentiesProject = skillsProject;

            List<Skill> skillLijst = projectrepository.Get(baseProjectId).Skill.ToList();
            foreach (Skill skill in project.Skill)
            {
                if (skillLijst.Contains(skill))
                {
                    skillLijst.Remove(skill);
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

            ProjectRepository projectrepository = new ProjectRepository((Context)Session["ProjectContext"]);
            List<Project> projectList = projectrepository.GetAll().ToList();

            SelectList projecten = new SelectList(projectrepository.AllOpen(projectList), "Id", "Name");
            ViewBag.projecten = projecten;
            return View();
        }
        public ActionResult GeselecteerdProjectWijzigen()
        {
            ProjectRepository projectrepository = new ProjectRepository((Context)Session["ProjectContext"]);
            Session["newProject"] = projectrepository.Get(Convert.ToInt32(Request.Form["Project"]));
            return RedirectToAction("ProjectAanmaken");
        }

        public ActionResult ProjectOpslaan(Project project)
        {
            if (Session["newProject"] == null)
            {
                Session["newProject"] = project;
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
            else if (Request.Form["toewijzentutor"] != null)
            {
                Session["tutor"] = Convert.ToInt32(Request.Form["tutor"]);
                return RedirectToAction("GroepToewijzenAanTutor");
            }


            return RedirectToAction("ProjectAanmaken");
        }

        [HttpPost]
        public ActionResult CompetentiesToevoegenAanLijst()
        {
            Project project = (Project)Session["newProject"];
            SkillRepository skillRepository = new SkillRepository((Context)Session["ProjectContext"]);

            string IdsVoorgaandProject = Request.Form["competentiesVoorgaandProject"];
            string IdsProject = Request.Form["competenties"];

            if (IdsVoorgaandProject != null)
            {
                string[] IdArrayVoorgaandProject = IdsVoorgaandProject.Split(',');

                foreach (string IdString in IdArrayVoorgaandProject)
                {
                    Skill toevoegSkill = skillRepository.Get(Convert.ToInt32(IdString));
                    project.Skill.Add(toevoegSkill);
                }
            }

            if (IdsProject != null)
            {
                string[] IdArrayProject = IdsProject.Split(',');
                foreach (string IdString in IdArrayProject)
                {
                    Skill verwijder = skillRepository.Get(Convert.ToInt32(IdString));
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
            SkillRepository skillRepository = new SkillRepository((Context)Session["ProjectContext"]);
            if (skill.Category != null)
            {
                Skill s = skillRepository.SkillExists(skill.Category);
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
                    if (!skillrepo.GetAll().Contains(s))
                    {
                        skillrepo.Insert(s);
                        skillrepo.Save();
                    }
                    project.Skill.Add(s);
                }
            }

            return RedirectToAction("CompetentiesToevoegenAanProject", (Project)null);
        }

        public ActionResult BeoordelingsmomentVerwijderen(int id)
        {
            System.Diagnostics.Debug.WriteLine(id);
            ProjectPeriodRepository projectperiodrepository = new ProjectPeriodRepository((Context)Session["ProjectContext"]);
            ProjectPeriod projectperiod = projectperiodrepository.Get(id);
            Project project = (Project)Session["newProject"];
            project.ProjectPeriod.Remove(projectperiod);
            projectperiodrepository.Delete(id);
            projectperiodrepository.Save();
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
            ProjectPeriod projectperiod = new ProjectPeriod() { Name = p.Name, Start = p.Start, End = p.End };
            Context context = (Context)Session["ProjectContext"];
            if (projectperiod.Name != null && projectperiod.Start != null && projectperiod.End != null)
            {
                ProjectPeriodRepository projectperiodrepository = new ProjectPeriodRepository(context);
                projectperiodrepository.Insert(projectperiod);
                projectperiodrepository.Save();
                project.ProjectPeriod.Add(projectperiod);
                Session["newProject"] = project;
            }
            return RedirectToAction("BeoordelingsmomentenToevoegen", "Docent");
        }

        public ActionResult ProjectToevoegen()
        {
            Project project = (Project)Session["newProject"];
            ProjectRepository projectrepository = new ProjectRepository((Context)Session["ProjectContext"]);
            projectrepository.Insert(project);
            projectrepository.Save();
            Session["newProject"] = null;
            return RedirectToAction("Index", "Docent");
        }

        public ActionResult SelecteerProject()
        {
            ProjectService projectservice = new ProjectService();
            ViewBag.selectListProjects = projectservice.GetTutorProject(getSessionDocentId());
            return View();
        }

        [HttpPost]
        public ActionResult SelecteerTutorGroep(int project)
        {
            ProjectService projectservice = new ProjectService();
            ViewBag.selectListGroups = projectservice.GetTutorGroup(project, getSessionDocentId());
            return View();
        }

        [HttpPost]
        public ActionResult ViewGroep(int groep)
        {
            ProjectService projectservice = new ProjectService();
            EvaluationService evaluationservice = new EvaluationService();
            Project project = projectservice.GetProjectFromGroup(groep);

            ViewBag.groupName = projectservice.GetGroupById(groep).Name;
            ViewBag.project = project;
            ViewBag.periodsCount = project.ProjectPeriod.Count;
            return View(evaluationservice.GetGroupEvaluation(groep));
        }

        public ActionResult GroepToewijzenAanTutor()
        {
            int tutorId = (int)Session["tutor"];
            GroupRepository grouprepository = new GroupRepository(new Context());
            ViewBag.addGroups = new MultiSelectList(grouprepository.NoTutor(), "Id", "Name");
            ViewBag.deleteGroups = new MultiSelectList(grouprepository.WithTutor(tutorId), "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult AddGroupTutor()
        {
            string varGroups = Request.Form["Groups"];
            List<string> groups = new List<string>();

            if (varGroups == null)
            {
                return RedirectToAction("GroepToewijzenAanTutor");
            }
            else if (varGroups.Length < 2)
            {
                groups.Add(varGroups);
            }
            else
            {
                groups = varGroups.Split(',').ToList(); ;
            }

            Context context = new Context();
            UserRepository userrepository = new UserRepository(context);
            User tutor = userrepository.Get(Convert.ToInt32(Session["tutor"]));
            GroupRepository grouprepository = new GroupRepository(context);
            foreach (string group in groups)
            {
                System.Diagnostics.Debug.WriteLine(group);
                grouprepository.Get(Convert.ToInt32(group)).Tutor = tutor;
            }
            context.SaveChanges();
            return RedirectToAction("GroepToewijzenAanTutor");

        }

        [HttpPost]
        public ActionResult DeleteGroupTutor()
        {
            string varGroups = Request.Form["Groups"];
            List<string> groups = new List<string>();

            if (varGroups == null)
            {
                return RedirectToAction("GroepToewijzenAanTutor");
            }
            else if (varGroups.Length < 2)
            {
                groups.Add(varGroups);
            }
            else
            {
                groups = varGroups.Split(',').ToList(); ;
            }

            Context context = new Context();
            GroupRepository grouprepository = new GroupRepository(context);
            foreach (string group in groups)
            {
                Group g = grouprepository.Get(Convert.ToInt32(group));
                System.Diagnostics.Debug.WriteLine("Groep: " + g.Name);
                var tutor = g.Tutor;
                g.Tutor = null;
                //System.Diagnostics.Debug.WriteLine("Groep: " + g.Name + " Tutor: " + g.Tutor.Name);
                grouprepository.Update(g);
                grouprepository.Save();
            }
            return RedirectToAction("GroepToewijzenAanTutor");

        }
    }
}
