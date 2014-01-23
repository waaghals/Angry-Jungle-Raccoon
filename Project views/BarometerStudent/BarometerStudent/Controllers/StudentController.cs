using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using BarometerStudent.Services;
using BarometerDomain.Model;

public class StudentController : Controller
{

    private int studentId = 2;
    private int userProjectId = 1;

    public ActionResult Index()
    {
        ProjectService ps = new ProjectService();
        SelectList sl = ps.WithStudent(studentId);
        ViewBag.projectId = sl;
        return View();
    }

    [HttpPost]
    public ActionResult ProjectOverzicht(int projectId)
    {
        ProjectService ps = new ProjectService();
        Project project = ps.GetProject(projectId);

        ViewBag.Project = project.Id;
        ViewBag.Title = project.Name;
        ViewBag.Description = project.Description;
        ViewBag.projectPeriodId = new SelectList(project.ProjectPeriod, "Id", "Name");
        ViewBag.Mededelingen = ps.GenerateAnnouncements(projectId);
        return View();
    }

    public ActionResult ProjectOverzicht()
    {
        TempData.Keep();
        ProjectService ps = new ProjectService();
        int projectId = Convert.ToInt32(TempData["ProjectId"]);
        Project project = ps.GetProject(projectId);

        ViewBag.Project = project.Id;
        ViewBag.Title = project.Name;
        ViewBag.Description = project.Description;
        ViewBag.projectPeriodId = new SelectList(project.ProjectPeriod, "Id", "Name");
        ViewBag.Mededelingen = ps.GenerateAnnouncements(projectId);
        return View();
    }

    [HttpPost]
    public ActionResult Beoordelen(int projectId, int projectPeriodId)
    {
        ProjectService ps = new ProjectService();
        EvaluationService es = new EvaluationService();
        Project project = ps.GetProject(projectId);
        Group group = ps.ByStudentAndProject(studentId, project.Id);
        List<Student> students = group.Student.ToList();
        List<Evaluation> evaluaties = new List<Evaluation>();
        List<List<Evaluation>> evaluationList = new List<List<Evaluation>>();
        List<string> names = new List<string>();

        if (Request.Form["beoordelen"] != null)
        {
            ViewBag.Beschrijving = "Hier beoordeel je je medestudenten";
            ViewBag.Actie = "geef";

            foreach (Student s in students)
            {
                if (s.Id != studentId)
                {
                    evaluaties = es.GetEvaluations(projectPeriodId, studentId, s.Id).ToList();
                    evaluationList.Add(evaluaties);
                    names.Add(s.Name);
                }
            }

            ViewBag.names = names;
        }
        else if (!project.Anonymous)
        {
            ViewBag.Beschrijving = "Hier zie je de beoordelingen van je medestudenten";
            ViewBag.Actie = "kijk";

            foreach (Student s in students)
            {
                if (s.Id != studentId)
                {
                    evaluaties = es.GetEvaluations(projectPeriodId, s.Id, studentId).ToList();
                    evaluationList.Add(evaluaties);
                    names.Add(s.Name);
                }
            }

            ViewBag.names = names;
        }
        else
        {
            return RedirectToAction("Index", "StudentHome");
        }

        foreach (List<Evaluation> eval in evaluationList)
        {
            foreach (Evaluation evalInner in eval)
            {
                evalInner.Grade /= 10;
            }
        }

        ViewBag.projectPeriodId = projectPeriodId;
        ViewBag.projectId = projectId;
        return View(evaluationList);
    }

    public ActionResult Beoordelen()
    {
        TempData.Keep();
        int projectId = Convert.ToInt32(TempData["ProjectId"]);
        int projectPeriodId = Convert.ToInt32(TempData["projectPeriodId"]);

        ProjectService ps = new ProjectService();
        EvaluationService es = new EvaluationService();
        Project project = ps.GetProject(projectId);
        Group group = ps.ByStudentAndProject(studentId, project.Id);
        List<Student> students = group.Student.ToList();
        List<Evaluation> evaluaties = new List<Evaluation>();
        List<List<Evaluation>> evaluationList = new List<List<Evaluation>>();
        List<string> names = new List<string>();

        ViewBag.Beschrijving = "Hier beoordeel je je medestudenten";
        ViewBag.Actie = "geef";

        foreach (Student s in students)
        {
            if (s.Id != studentId)
            {
                evaluaties = es.GetEvaluations(projectPeriodId, studentId, s.Id).ToList();
                evaluationList.Add(evaluaties);
                names.Add(s.Name);
            }
        }

        ViewBag.names = names;


        foreach (List<Evaluation> eval in evaluationList)
        {
            foreach (Evaluation evalInner in eval)
            {
                evalInner.Grade /= 10;
            }
        }
        ViewBag.warning = "Zorg dat alle Evaluatie cijfers tussen de 1 en de 10 zitten";
        ViewBag.projectPeriodId = projectPeriodId;
        ViewBag.projectId = projectId;
        return View(evaluationList);
    }

    [HttpPost]
    public ActionResult Evaluate(List<List<Evaluation>> model)
    {
        if (Request.Form["annuleren"] != null)
        {
            TempData["ProjectId"] = Request.Form["projectIdFrom"];
            return RedirectToAction("ProjectOverzicht");
        }
        else
        {
            foreach (List<Evaluation> list in model)
                foreach (Evaluation evaluation in list)
                {
                    if (evaluation.Grade < 0 || evaluation.Grade > 10)
                    {
                        TempData["ProjectPeriodId"] = Request.Form["projectPeriodIdFrom"];
                        TempData["ProjectId"] = Request.Form["projectIdFrom"];
                        return RedirectToAction("Beoordelen");
                    }
                }
            EvaluationService es = new EvaluationService();
            es.Evaluate(model);
            TempData["ProjectId"] = Request.Form["projectIdFrom"];
            return RedirectToAction("ProjectOverzicht");
        }
    }

    public ActionResult VoortgangInzien(int studentId)
    {
        EvaluationService es = new EvaluationService();
        Dictionary<string, List<Evaluation>> projectsAvgEvaluations = es.GetAvgEvaluations(studentId);
        ViewBag.projectNames = projectsAvgEvaluations.Keys.ToList();
        return View(projectsAvgEvaluations);
    }
}

