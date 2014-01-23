using BarometerDomain.Model;
using BarometerStudent.Services;
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
        private int studentID;

        public ActionResult Menu()
        {
            return View();
        }

        private ProjectController()
        {
            studentID = ((User)this.Session["user"]).Id;
        }

        [HttpPost]
        public ActionResult ProjectOverzicht()
        {
            int id = Convert.ToInt32(Request.Form["Project"]);
            ProjectService ps = new ProjectService();
            Project project = ps.GetProject(id);

            ViewBag.Project = project.Id;
            ViewBag.Title = project.Name;
            ViewBag.Description = project.Description;
            ViewBag.ProjectPeriod = new SelectList(project.ProjectPeriod, "Id", "Name");
            ViewBag.Mededelingen = ps.GenerateAnnouncements(id);
            return View();
        }

        [HttpPost]
        public ActionResult Beoordelen()
        {
            ProjectService ps = new ProjectService();
            EvaluationService es = new EvaluationService();
            Project project = ps.GetProject(Convert.ToInt32(Request.Form["SelectedProject"]));
            Group group = ps.ByStudentAndProject(studentID, project.Id);
            List<Student> students = group.Student.ToList();
            List<Evaluation> evaluaties = new List<Evaluation>();
            List<List<Evaluation>> evaluationList = new List<List<Evaluation>>();
            List<string> names = new List<string>();
            string periodName = Request.Form["ProjectPeriod"];
            string name = project.Name;

            if (Request.Form["beoordelen"] != null)
            {
                ViewBag.Beschrijving = "Hier beoordeel je je medestudenten";
                ViewBag.Actie = "geef";
                ViewBag.Title = "Beoordelen van Project " + name + " in periode " + periodName;

                foreach (Student s in students)
                {
                    if (s.Id != studentID)
                    {
                        evaluaties = es.GetEvaluations(Convert.ToInt32(periodName), studentID, s.Id).ToList();
                        evaluationList.Add(evaluaties);
                        names.Add(s.Name);
                    }
                }

                ViewBag.names = names;
            }
            else if (!project.Anonymous)
            {
                ViewBag.Beschrijving = "Hier zie je de beoordelingen van je medestudenten";
                ViewBag.Actie = "kijk";
                ViewBag.Title = "Bekijken van Beoordelingen van Project " + name + " in periode " + periodName;

                foreach (Student s in students)
                {
                    if (s.Id != studentID)
                    {
                        evaluaties = es.GetEvaluations(Convert.ToInt32(periodName), s.Id, studentID).ToList();
                        evaluationList.Add(evaluaties);
                        names.Add(s.Name);
                    }
                }

                ViewBag.names = names;
            }
            else
            {
                return RedirectToAction("Index","StudentHome");
            }

            foreach (List<Evaluation> eval in evaluationList)
            {
                foreach (Evaluation evalInner in eval)
                {
                    evalInner.Grade /= 10;
                }
            }

            return View(evaluationList);
        }

        [HttpPost]
        public ActionResult Evaluate(List<List<Evaluation>> model)
        {
            if (Request.Form["annuleren"] != null)
            {
                return RedirectToAction("Index", "StudentHome");
            }
            else
            {
                EvaluationService es = new EvaluationService();
                es.Evaluate(model);
                return RedirectToAction("Index", "StudentHome");
            }
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
