using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BarometerDomain.Model;
using BarometerDomain.Repositories;
using BarometerDomain;
using BarometerStudent.Services;


namespace BarometerStudent.Controllers
{
    public class StudentHomeController : Controller
    {
        //private ProjectRepository pr = new ProjectRepository(new Context());
        //
        // GET: /StudentHome/

        public ActionResult Index()
        {
            //zoek de user
            /*
            UserService uService = new UserService();
            User user = uService.Login(studentNumber, login);
            Session["User"] = user; //sla de user op in de sessie
            if(user.RoleType.Contains(RoleType.Teacher) || user.RoleType.Contains(RoleType.Administrator))
                RedirectToAction("Menu","Docent"); //redirect de user naar de juiste startpagina gebaseerd op login data
            */

            using (var db = new BarometerDomain.Context())
            {
                ProjectRepository pr = new ProjectRepository(db);
                SelectList sl = new SelectList(pr.WithStudent(2), "Id", "Name");

                ViewBag.Project = sl;
            }
            return View();
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
