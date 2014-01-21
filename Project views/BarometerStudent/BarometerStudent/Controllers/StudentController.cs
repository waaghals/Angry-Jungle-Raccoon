using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using BarometerStudent.Services;
using BarometerDomain.Model;

public class StudentController : Controller
{

    public ActionResult VoortgangInzien(int studentId)
    {
        EvaluationService es = new EvaluationService();
        Dictionary<string, List<Evaluation>> projectsAvgEvaluations = es.GetAvgEvaluations(studentId);
        ViewBag.projectNames = projectsAvgEvaluations.Keys.ToList();
        return View(projectsAvgEvaluations);
    }
}

