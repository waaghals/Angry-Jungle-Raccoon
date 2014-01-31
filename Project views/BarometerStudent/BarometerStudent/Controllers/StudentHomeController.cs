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
                Student s = getSessionStudent();
                SelectList sl = new SelectList(pr.WithStudent(s.Id), "Id", "Name");

                ViewBag.Project = sl;
            }
            return View();
        }

        private Student getSessionStudent()
        {
            StudentRepository studentrep = new StudentRepository(new Context());
            Student student = studentrep.Get(((User)HttpContext.Session["User"]).Id);
            return student;
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
