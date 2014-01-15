using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using BarometerDomain.Model;
using BarometerDomain.Repositories;
using BarometerDomain;

public class StudentController : Controller
{

    public ActionResult VoortgangInzien(int studentId)
    {
        StudentRepository studentRepo = new StudentRepository(new Context());
        Student student = studentRepo.Get(studentId);

        //List projectnames
        IList<string> projectNames = new List<string>();
        List<Evaluation> evaluationList = new List<Evaluation>();
        Dictionary<string, List<Evaluation>> projectsAvgEvaluations = new Dictionary<string, List<Evaluation>>();
        foreach(Group group in student.Groups)
        {
            foreach(Project project in group.Project)
            {
                projectNames.Add(project.Name);
                evaluationList = (List<Evaluation>) project.GetAverageEvaluations(student);
                projectsAvgEvaluations.Add(project.Name, evaluationList);
            }
        }
        ViewBag.projectNames = projectNames;
        
        return View(projectsAvgEvaluations);
    }
}

