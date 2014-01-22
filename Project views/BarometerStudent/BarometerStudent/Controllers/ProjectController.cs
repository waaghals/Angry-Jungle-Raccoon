using BarometerDomain;
using BarometerDomain.Model;
using BarometerDomain.Repositories;
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
        private int studentID = 2;
        private int userProjectID = 1;

        

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
                ps.DeleteGroup(Convert.ToInt32(group), userProjectID);
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
                ps.AddGroup(Convert.ToInt32(group), userProjectID);
            }
            return RedirectToAction("GroepToewijzenAanProject");

        }


    }
}
