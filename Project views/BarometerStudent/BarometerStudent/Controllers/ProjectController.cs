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
            ViewBag.Title = "Project Barometer - Ontwerp";
            ViewBag.Description = " Lorem ipsum dolor sit amet, consectetur adipiscing elit fusce vel sapien elit in malesuada semper mi, id sollicitudin urna fermentum ut fusce varius nisl ac ipsum gravida vel pretium tellus tincidunt integer eu augue augue nunc elit dolor, luctus placerat scelerisque euismod, iaculis eu lacus nunc mi elit, vehicula ut.<br />Lorem ipsum dolor sit amet, consectetur adipiscing elit fusce vel sapien elit in malesuada semper mi, id sollicitudin urna fermentum ut fusce varius nisl ac ipsum gravida vel pretium tellus.";
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

        [HttpPost]
        public ActionResult Beoordelen(String beoordelen, int SelectedProject, String week)
        {
            ViewBag.Title = "Beoordelen van Project " + SelectedProject + " in week " + week;
            if (beoordelen != null)
            {
                ViewBag.Beschrijving = "Hier beoordeel je je medestudenten";
                ViewBag.Studenten = new string[] {
                "Henk",
                "Piet",
                "Klaas"
            };
            }
            else
            {
                ViewBag.Beschrijving = "Hier zie je de beoordelingen van je medestudenten";
                ViewBag.Studenten = new string[] {
                "Henk",
                "Piet",
                "Klaas"
            };
            }
            return View();
        }
    }
}
