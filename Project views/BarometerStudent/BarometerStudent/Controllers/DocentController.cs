﻿using BarometerDomain;
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

        [HttpGet]
        public ActionResult SelecteerProject()
        {
            ProjectRepository pr = new ProjectRepository(new Context());
            SelectList sl = new SelectList(pr.ByTutor(/*tutorid*/1), "Id", "Name");
            ViewBag.project = sl;
            return View();
        }

        [HttpPost]
        public ActionResult SelecteerProject(Project project)
        {
            if (ModelState.IsValid)
            {
                TempData["myProject"] = project;
                return RedirectToAction("SelecteerTutorGroep", "Docent");
            }
            return View();
        }

        [HttpGet]
        public ActionResult SelecteerTutorGroep()
        {
            if (TempData.ContainsKey("myProject"))
            {
                Project myProject = (Project)TempData["myProject"];
                ViewBag.curProject = myProject;
                List<Group> tutorGroupList = new List<Group>();
                foreach(Group group in myProject.Groups)
                {
                    if (group.Tutor.Id == userID)
                    {
                        tutorGroupList.Add(group);
                    }
                }
                SelectList sl = new SelectList(tutorGroupList, "Id", "Name");
                ViewBag.groups = sl; 
                return View();
            }
            else
            {
                return RedirectToAction("SelecteerProject", "Docent");
            }
        }

        [HttpPost]
        public ActionResult SelecteerTutorgroep(Group group)
        {
            return View();
        }
    }
}