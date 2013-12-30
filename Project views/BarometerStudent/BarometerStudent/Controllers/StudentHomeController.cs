using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BarometerDomain.Model;


namespace BarometerStudent.Controllers
{
    public class StudentHomeController : Controller
    {
        //
        // GET: /StudentHome/

        public ActionResult Index()
        {
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
