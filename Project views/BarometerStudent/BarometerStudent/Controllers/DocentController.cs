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

        public ActionResult SelecteerProject()
        {
            ProjectRepository pr = new ProjectRepository(new Context());

            ViewBag.selectListProjects = new SelectList(pr.ByTutor(/*tutorid*/userID), "Id", "Name", "1");
            return View();
        }

        [HttpPost]
        public ActionResult SelecteerProject(string project)
        {
            if (project != null)
            {
                int projectId = Convert.ToInt32(project);

                ProjectRepository pr = new ProjectRepository(new Context());
                Project myProject = pr.Get(projectId);

                TempData["myProject"] = myProject;

                return RedirectToAction("SelecteerTutorGroep", "Docent");
            }
            return View();
        }

        public ActionResult SelecteerTutorGroep()
        {
            if (TempData.ContainsKey("myProject"))
            {
                TempData.Keep();

                Project myProject = (Project)TempData["myProject"];
                IList<Group> tutorGroupList = new List<Group>();

                foreach(Group group in myProject.Groups)
                {
                    if (group.Tutor.Id == userID)
                    {
                        tutorGroupList.Add(group);
                    }
                }
                SelectList sl = new SelectList(tutorGroupList, "Id", "Name");

                ViewBag.selectListGroups = sl;
                return View();
            }
            else
            {
                return RedirectToAction("SelecteerProject", "Docent");
            }
        }

        [HttpPost]
        public ActionResult SelecteerTutorgroep(string group)
        {
            if (group != null)
            {
                int groupId = Convert.ToInt32(group);

                GroupRepository gr = new GroupRepository(new Context());
                Group myGroup = gr.Get(groupId);

                TempData["myGroup"] = myGroup;

                return RedirectToAction("ViewGroep", "Docent");
            }
            return View();
        }

        public ActionResult ViewGroep()
        {
            if (TempData.ContainsKey("myGroup") && TempData.ContainsKey("myProject"))
            {
                TempData.Keep();

                Group group = (Group)TempData["myGroup"];
                Project project = (Project)TempData["myProject"];

                List<Student> studentList = new List<Student>();
                
                foreach(Student student in group.Student)
                {
                    if(!(studentList.Contains(student)))
                    {
                        studentList.Add(student);
                    }
                }
                
                ViewBag.studenten = studentList;
                ViewBag.project = project;
                ViewBag.group = group;
                return View();
            }
            else
            {
                return RedirectToAction("SelecteerProject", "Docent");
            }
        }
    }
}
