using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BarometerDomain.Model;
using BarometerDomain.Repositories;
using BarometerDomain;


namespace BarometerStudent.Controllers
{
    public class StudentHomeController : Controller
    {
        //private ProjectRepository pr = new ProjectRepository(new Context());
        //
        // GET: /StudentHome/

        public ActionResult Index()
        {
            List<Project> test = new List<Project>();
            test.Add(new Project()); 
            test.Add(new Project());
            test[0].Id = 1;
            test[1].Id = 2;
            test[0].Name = "Project 1";
            test[1].Name = "Project 2";
            using (var db = new BarometerDomain.Context())
            {
                ProjectRepository pr = new ProjectRepository(db);
                SelectList sl = new SelectList(pr.WithStudent(2), "Id", "Name");

                ViewBag.Project = sl;
            }
            return View();
            //TEST CODE
            Session["User"] = new User() { };
        }

        public ActionResult Create(/* viewModel model */)
        {
            //
            //ingevoegde waarden uit het model in de database plaatsen
            return View();
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        public ActionResult OverView()
        {
            return View();
        }

        public ActionResult Progress(int id)
        {
            return View();
        }

        public ActionResult Read(int id)
        {
            return View();
        }

        public ActionResult Update(int id /*, viewModel object */)
        {
            return View();
        }
    }
}
