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

        private List<string> competenties = new List<string>();

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

        public ActionResult Beoordelen()
        {
            ViewBag.Title = "Beoordelen";
            ViewBag.Studenten = new string[] {
                "Henk",
                "Piet",
                "Klaas"
            };
            return View();
        }

        public ActionResult NieuwProjectAanmaken()
        {
            ViewBag.Competenties = new string[] {
                "Competentie 1",
                "Competentie 2",
                "Competentie 3",
                "Competentie 4",
                "Competentie 5",
                "Competentie 6",
                "Competentie 7",
                "Competentie 8",
                "Competentie 9",
                "Competentie 10"
            };
            return View();
        }

        public ActionResult TutorBeoordelingenInzien()
        {
            ViewBag.Groepen = new string[] { "Groep 1", "Groep 2", "Groep 3", "Groep 4" };
            ViewBag.BeoordelingsMomenten = new string[] { "Week 3", "Week 6", "Week 9" };
            ViewBag.Studenten = new string[] { "Henk", "Piet", "Klaas", "Harry" };
            ViewBag.Competenties = new string[] {
                "Competentie 1",
                "Competentie 2",
                "Competentie 3",
                "Competentie 4",
                "Competentie 5",
                "Competentie 6",
                "Competentie 7",
                "Competentie 8",
                "Competentie 9",
                "Competentie 10"
            };
            return View();
        }

        public ActionResult GroepToewijzenAanProject()
        {
            ViewBag.Groepen = new string[] { "Groep 1", "Groep 2", "Groep 3", "Groep 4", "Groep 5" };
            return View();
        }

        public ActionResult TutorToewijzen()
        {
            ViewBag.Groepen = new string[] { "Groep 1", "Groep 2", "Groep 3", "Groep 4", "Groep 5" };
            ViewBag.Docenten = new string[] { "Docent 1", "Docent 2", "Docent 3", "Docent 4", "Docent 5", "Docent 6" };
            return View();
        }

        public ActionResult OpmerkingScherm()
        {
            ViewBag.Opmerkingen = new string[] { "Opmerking 1", "Opmerking 2", "Opmerking 3", "Opmerking 4", "Opmerking 5" };
            return View();
        }

        public ActionResult ProjectAanmaken()
        {
            ViewBag.Competenties = new string[] {
                "Competentie 1",
                "Competentie 2",
                "Competentie 3",
                "Competentie 4",
                "Competentie 5",
                "Competentie 6"
            };
            return View();
        }

        public ActionResult StudentCijferToekennen()
        {
            ViewBag.Groepen = new string[] { "Groep 1", "Groep 2", "Groep 3", "Groep 4" };
            ViewBag.BeoordelingsMomenten = new string[] { "Week 3", "Week 6", "Week 9" };
            ViewBag.Studenten = new string[] { "Henk", "Piet", "Klaas", "Harry" };
            ViewBag.Competenties = new string[] {
                "Competentie 1",
                "Competentie 2",
                "Competentie 3",
                "Competentie 4",
                "Competentie 5",
                "Competentie 6",
                "Competentie 7",
                "Competentie 8",
                "Competentie 9",
                "Competentie 10"
            };
            return View();
        }

        public ActionResult OnderbouwingAanvragen()
        {
            ViewBag.Groepen = new string[] { "Groep 1", "Groep 2", "Groep 3", "Groep 4" };
            ViewBag.BeoordelingsMomenten = new string[] { "Week 3", "Week 6", "Week 9" };
            ViewBag.Studenten = new string[] { "Henk", "Piet", "Klaas", "Harry" };
            ViewBag.Competenties = new string[] {
                "Competentie 1",
                "Competentie 2",
                "Competentie 3",
                "Competentie 4",
                "Competentie 5",
                "Competentie 6",
                "Competentie 7",
                "Competentie 8",
                "Competentie 9",
                "Competentie 10"
            };
            return View();
        }

        public ActionResult MentorVoortgangInzien()
        {
            ViewBag.Groepen = new string[] { "Groep 1", "Groep 2", "Groep 3", "Groep 4" };
            ViewBag.BeoordelingsMomenten = new string[] { "Week 3", "Week 6", "Week 9" };
            ViewBag.Studenten = new string[] { "Henk", "Piet", "Klaas", "Harry" };
            ViewBag.Competenties = new string[] {
                "Competentie 1",
                "Competentie 2",
                "Competentie 3",
                "Competentie 4",
                "Competentie 5",
                "Competentie 6",
                "Competentie 7",
                "Competentie 8",
                "Competentie 9",
                "Competentie 10"
            };
            return View();
        }

        public ActionResult KlachtIndienen()
        {
            ViewBag.Groepen = new string[] { "Groep 1", "Groep 2", "Groep 3", "Groep 4" };
            ViewBag.BeoordelingsMomenten = new string[] { "Week 3", "Week 6", "Week 9" };
            ViewBag.Studenten = new string[] { "Henk", "Piet", "Klaas", "Harry" };
            ViewBag.Competenties = new string[] {
                "Competentie 1",
                "Competentie 2",
                "Competentie 3",
                "Competentie 4",
                "Competentie 5",
                "Competentie 6",
                "Competentie 7",
                "Competentie 8",
                "Competentie 9",
                "Competentie 10"
            };
            return View();
        }

        public ActionResult GroepsindelingAanmaken()
        {
            return View();
        }

        public ActionResult StudentNonActiefZetten()
        {
            return View();
        }

    }
}
