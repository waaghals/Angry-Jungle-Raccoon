using BarometerDomain;
using BarometerDomain.Model;
using BarometerDomain.Repositories;
using System;
using System.Collections.Generic;
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
        private int studentID = 1;

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
        public ActionResult Beoordelen()
        {
            Project project = pr.Get(Convert.ToInt32(Request.Form["SelectedProject"]));
            Group group = new Group();
            string name = project.Name;

            foreach(Group g in project.Groups)
            {
                if (g.Id == pr.ByStudentAndProject(studentID, project.Id).Id)
                {
                    group = g;
                }
            }

            string periodName = "";

            foreach (ProjectPeriod p in project.ProjectPeriod)
            {
                if (p.Id == Convert.ToInt32(Request.Form["ProjectPeriod"]))
                {
                    periodName = p.Name;
                }
            }

            ViewBag.Title = "Beoordelen van Project " + name + " in periode " + periodName;
            if (Request.Form["beoordelen"] != null)
            {
                ViewBag.Beschrijving = "Hier beoordeel je je medestudenten";
            }
            else
            {
                ViewBag.Beschrijving = "Hier zie je de beoordelingen van je medestudenten";
            }

            List<string> names = new List<string>();

            foreach (Student s in group.Student)
            {
                if (studentID != s.Id)
                {
                    names.Add(s.Name);
                }
            }
            ViewBag.studenten = names;

            List<string> skills = new List<string>();

            foreach (Skill s in project.Skill)
            {
                skills.Add(s.Category);
            }
            ViewBag.skills = skills;

            ViewBag.byStudent = studentID;

            StudentRepository sr = new StudentRepository(new Context());
            TempData["curStudent"] = sr.Get(studentID);

            return View();
        }

        [HttpPost]
        public ActionResult Evaluate(ProjectPeriod pp)
        {

            String[] tst = Request.Form.AllKeys;
            List<Evaluation> evaluations = (List<Evaluation>) pp.Evaluation;
            foreach (Evaluation e in evaluations)
            {
                if (TempData.ContainsKey("curStudent"))
                {
                    e.By = (Student)TempData["curStudent"];
                    //e.For = RequestForm[""];
                }
            }
            return View();
        }

        public ActionResult Menu()
        {
            return View();
        }

        public ActionResult GroepsindelingAanmaken()
        {
            return View();
        }

    }
}
