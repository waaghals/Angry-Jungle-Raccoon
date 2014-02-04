using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using BarometerStudent.Services;
using BarometerDomain.Model;
using BarometerDomain.Repositories;
using BarometerDomain;

public class StudentController : Controller
{
    private int getSessionStudentId()
    {
        StudentRepository studentrep = new StudentRepository(new Context());
        Student student = studentrep.Get(((User)HttpContext.Session["User"]).Id);
        return student.Id;
    }

    public ActionResult Index()
    {
        ProjectService ps = new ProjectService();
        SelectList sl = ps.WithStudent(getSessionStudentId());
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
        int projectId = Convert.ToInt32(TempData["projectId"]);
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
        Group group = ps.ByStudentAndProject(getSessionStudentId(), project.Id);
        List<Student> students = group.Student.ToList();
        List<Evaluation> evaluaties = new List<Evaluation>();
        List<List<Evaluation>> evaluationList = new List<List<Evaluation>>();
        List<string> names = new List<string>();

        ProjectPeriod pp = new ProjectPeriod();
        foreach (ProjectPeriod p in project.ProjectPeriod)
        {
            if (p.Id==projectPeriodId)
            {
                pp = p;
            }
        }

        if (Request.Form["beoordelen"] != null && (DateTime.Now < pp.End && DateTime.Now > pp.Start))
        {
            ViewBag.Beschrijving = "Hier beoordeel je je medestudenten";
            ViewBag.Actie = "geef";

            foreach (Student s in students)
            {
                if (s.Id != getSessionStudentId())
                {
                    evaluaties = es.GetEvaluations(projectPeriodId, getSessionStudentId(), s.Id).ToList();
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
                if (s.Id != getSessionStudentId())
                {
                    evaluaties = es.GetEvaluations(projectPeriodId, s.Id, getSessionStudentId()).ToList();
                    evaluationList.Add(evaluaties);
                    names.Add(s.Name);
                }
            }

            ViewBag.names = names;
        }
        else
        {
            return RedirectToAction("Index", "Student");
        }

        foreach (List<Evaluation> eval in evaluationList)
        {
            foreach (Evaluation evalInner in eval)
            {
                evalInner.Grade /= 10;
            }
        }

        TempData["projectPeriodId"] = projectPeriodId;
        TempData["projectId"] = projectId;
        return View(evaluationList);
    }

    public ActionResult Beoordelen()
    {
        TempData.Keep();
        int projectId = Convert.ToInt32(TempData["projectId"]);
        int projectPeriodId = Convert.ToInt32(TempData["projectPeriodId"]);

        ProjectService ps = new ProjectService();
        EvaluationService es = new EvaluationService();
        Project project = ps.GetProject(projectId);
        Group group = ps.ByStudentAndProject(getSessionStudentId(), project.Id);
        List<Student> students = group.Student.ToList();
        List<Evaluation> evaluaties = new List<Evaluation>();
        List<List<Evaluation>> evaluationList = new List<List<Evaluation>>();
        List<string> names = new List<string>();

        ViewBag.Beschrijving = "Hier beoordeel je je medestudenten";
        ViewBag.Actie = "geef";

        foreach (Student s in students)
        {
            if (s.Id != getSessionStudentId())
            {
                evaluaties = es.GetEvaluations(projectPeriodId, getSessionStudentId(), s.Id).ToList();
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
        TempData["projectPeriodId"] = projectPeriodId;
        TempData["projectId"] = projectId;
        return View(evaluationList);
    }

    [HttpPost]
    public ActionResult Evaluate(List<List<Evaluation>> model)
    {
        TempData.Keep();
        if (Request.Form["annuleren"] != null)
        {
            return RedirectToAction("ProjectOverzicht");
        }
        else
        {
            foreach (List<Evaluation> list in model)
                foreach (Evaluation evaluation in list)
                {
                    if (evaluation.Grade < 0 || evaluation.Grade > 10)
                    {
                        return RedirectToAction("Beoordelen");
                    }
                }
            EvaluationService es = new EvaluationService();
            es.Evaluate(model);
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

