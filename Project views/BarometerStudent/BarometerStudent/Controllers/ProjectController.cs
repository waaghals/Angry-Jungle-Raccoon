using BarometerDomain;
using BarometerDomain.Model;
using BarometerDomain.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BarometerStudent.Controllers
{
    public class ProjectController : Controller
    {

        //
        // GET: /Project/

        private ProjectRepository pr = new ProjectRepository(new Context());
        private int studentID = 2;

        public ActionResult ProjectOverzicht()
        {
            ViewBag.Title = "Project Barometer - Ontwerp";
            ViewBag.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit fusce vel sapien elit in malesuada semper mi, id sollicitudin urna fermentum ut fusce varius nisl ac ipsum gravida vel pretium tellus tincidunt integer eu augue augue nunc elit dolor, luctus placerat scelerisque euismod, iaculis eu lacus nunc mi elit, vehicula ut.<br />Lorem ipsum dolor sit amet, consectetur adipiscing elit fusce vel sapien elit in malesuada semper mi, id sollicitudin urna fermentum ut fusce varius nisl ac ipsum gravida vel pretium tellus.";
            ViewBag.Mededelingen = new string[] {
                "25/10/2013 - Beoordelingsmoment week 9 is gesloten.",
                "24/10/2013 - Waarschuwing, er is nog maar één dag om de beoordelingen in te vullen.",
                "21/10/2013 - Beoordelingsmoment week 9 is opengesteld."
            };
            ViewBag.BeoordelingsMomenten = new string[] {
                "Week 3",
                "Week 6",
                "Week 9"
            };
            return View();
        }

        [HttpPost]
        public ActionResult ProjectOverzicht(int Project)
        {
            ViewBag.Project = Project;

            ViewBag.Title = pr.Get(Project).Name;
            ViewBag.Description = pr.Get(Project).Description;
            ViewBag.ProjectPeriod = new SelectList(pr.Get(Project).ProjectPeriod, "Id", "Name");

            ViewBag.Mededelingen = new ProjectRepository(new Context()).GenerateAnnouncements(Project);
            return View();
        }

        [HttpPost]
        public ActionResult Beoordelen()
        {
            ProjectPeriodRepository ppr = new ProjectPeriodRepository(new Context());

            Project project = pr.Get(Convert.ToInt32(Request.Form["SelectedProject"]));
            Group group = pr.ByStudentAndProject(studentID, project.Id);
            List<Student> students = (List<Student>)group.Student;
            List<Evaluation> evaluaties = new List<Evaluation>();
            List<List<Evaluation>> evallist = new List<List<Evaluation>>();
            List<string> names = new List<string>();

            foreach (Student s in students)
            {
                if (s.Id != studentID)
                {
                    evaluaties = (List<Evaluation>)ppr.GetEvaluations(Convert.ToInt32(Request.Form["ProjectPeriod"]), studentID, s.Id);
                    evallist.Add(evaluaties);
                    names.Add(s.Name);
                }
            }
            ViewBag.names = names;

            string name = project.Name;
            string periodName = Request.Form["ProjectPeriod"];
            ViewBag.Title = "Beoordelen van Project " + name + " in periode " + periodName;
            if (Request.Form["beoordelen"] != null)
            {
                ViewBag.Beschrijving = "Hier beoordeel je je medestudenten";
            }
            else
            {
                ViewBag.Beschrijving = "Hier zie je de beoordelingen van je medestudenten";
            }

            foreach(List<Evaluation> eval in evallist)
            {
                foreach(Evaluation evalInner in eval)
                {
                    evalInner.Grade /= 10;
                }
            }

            return View(evallist);
        }

        [HttpPost]
        public ActionResult Evaluate(List<List<Evaluation>> model)
        {
            Context context = new Context();
            if (Request.Form["annuleren"] != null)
            {
                return RedirectToAction("Index", "StudentHome");
            }
            else
            {
                List<Evaluation> update = new List<Evaluation>();
                foreach (List<Evaluation> l in model)
                    foreach (Evaluation e in l)
                    {
                        // bij het invoegen keer 10
                        e.Grade *= 10;
                        update.Add(e);
                    }

                SkillRepository sr = new SkillRepository(context);
                StudentRepository str = new StudentRepository(context);
                EvaluationRepository er = new EvaluationRepository(context);

                foreach (Evaluation e in update)
                {
                    e.Skill = sr.Get(e.Skill.Id);
                    e.By = str.Get(e.By.Id);
                    e.For = str.Get(e.For.Id);
                    er.Update(e);
                }

                er.Save();
                sr.Save();
                str.Save();


                return RedirectToAction("Index", "StudentHome");
            }
        }

        public ActionResult Menu()
        {
            return View();
        }

        public ActionResult GroepsindelingAanmaken()
        {
            /**/
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
                //zet het in de db
                new Services.ExcelReader(path).ToDatabase();
                //verwijder het
                System.IO.File.Delete(path);
                
                
            }
            return View("GroepsindelingAanmaken");
        }

    }
}
