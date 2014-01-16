using BarometerDomain;
using BarometerDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BarometerDomain.Model;

namespace BarometerStudent.Controllers
{
    public class DocentController : Controller
    {
        //
        // GET: /Docent/
        private int userID = 1;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MentorStudenten()
        {
            UserRepository userrep = new UserRepository(new Context());
            User user = userrep.Get(userID);
            List<Student> studenten = user.MentorStudent.ToList<Student>();
            return View(studenten);
        }

    }
}
