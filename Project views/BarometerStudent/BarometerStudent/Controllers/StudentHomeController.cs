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
            return RedirectToAction("Index", "Student");
        }
    }
}
