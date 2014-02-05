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
        StudentRepository studentrepository = new StudentRepository(new Context());
        Student student = studentrepository.Get(((User)HttpContext.Session["User"]).Id);
        return student.Id;
    }

    public ActionResult Index()
    {
        ProjectService projectservice = new ProjectService();
        SelectList sl = projectservice.WithStudent(getSessionStudentId());
        ViewBag.projectId = sl;
        return View();
    }

    [HttpPost]
    public ActionResult ProjectOverzicht(int projectId)
    {
        ProjectService projectservice = new ProjectService();
        Project project = projectservice.GetProject(projectId);

        ViewBag.Project = project.Id;
        ViewBag.Title = project.Name;
        ViewBag.Description = project.Description;
        ViewBag.projectPeriodId = new SelectList(project.ProjectPeriod, "Id", "Name");
        ViewBag.Mededelingen = projectservice.GenerateAnnouncements(projectId);
        return View();
    }

    public ActionResult ProjectOverzicht()
    {
        TempData.Keep();
        ProjectService projectservice = new ProjectService();
        int projectId = Convert.ToInt32(TempData["projectId"]);
        Project project = projectservice.GetProject(projectId);

        ViewBag.Project = project.Id;
        ViewBag.Title = project.Name;
        ViewBag.Description = project.Description;
        ViewBag.projectPeriodId = new SelectList(project.ProjectPeriod, "Id", "Name");
        ViewBag.Mededelingen = projectservice.GenerateAnnouncements(projectId);
        return View();
    }

    [HttpPost]
    public ActionResult Beoordelen(int projectId, int projectPeriodId)
    {
        ProjectService projectservice = new ProjectService();
        EvaluationService evaluationservice = new EvaluationService();
        Project project = projectservice.GetProject(projectId);
        Group group = projectservice.ByStudentAndProject(getSessionStudentId(), project.Id);
        List<Student> students = group.Student.ToList();
        List<Evaluation> evaluaties = new List<Evaluation>();
        List<List<Evaluation>> evaluationList = new List<List<Evaluation>>();
        List<string> names = new List<string>();

        ProjectPeriod projectperiod = new ProjectPeriod();
        foreach (ProjectPeriod partperiod in project.ProjectPeriod)
        {
            if (partperiod.Id==projectPeriodId)
            {
                projectperiod = partperiod;
            }
        }

        if (Request.Form["beoordelen"] != null && (DateTime.Now < projectperiod.End && DateTime.Now > projectperiod.Start))
        {
            ViewBag.Beschrijving = "Hier beoordeel je je medestudenten";
            ViewBag.Actie = "geef";

            foreach (Student student in students)
            {
                if (student.Id != getSessionStudentId())
                {
                    evaluaties = evaluationservice.GetEvaluations(projectPeriodId, getSessionStudentId(), student.Id).ToList();
                    evaluationList.Add(evaluaties);
                    names.Add(student.Name);
                }
            }

            ViewBag.names = names;
        }
        else if (!project.Anonymous && Request.Form["beoordelen"] == null)
        {
            ViewBag.Beschrijving = "Hier zie je de beoordelingen van je medestudenten";
            ViewBag.Actie = "kijk";

            foreach (Student student in students)
            {
                if (student.Id != getSessionStudentId())
                {
                    evaluaties = evaluationservice.GetEvaluations(projectPeriodId, student.Id, getSessionStudentId()).ToList();
                    evaluationList.Add(evaluaties);
                    names.Add(student.Name);
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

        ViewBag.Beschrijving += " voor Project " + project.Name + " in Periode " + projectperiod.Name + ".";

        TempData["projectPeriodId"] = projectPeriodId;
        TempData["projectId"] = projectId;
        return View(evaluationList);
    }

    public ActionResult Beoordelen()
    {
        TempData.Keep();
        int projectId = Convert.ToInt32(TempData["projectId"]);
        int projectPeriodId = Convert.ToInt32(TempData["projectPeriodId"]);

        ProjectService projectservice = new ProjectService();
        EvaluationService evaluationservice = new EvaluationService();
        Project project = projectservice.GetProject(projectId);
        Group group = projectservice.ByStudentAndProject(getSessionStudentId(), project.Id);
        List<Student> students = group.Student.ToList();
        List<Evaluation> evaluaties = new List<Evaluation>();
        List<List<Evaluation>> evaluationList = new List<List<Evaluation>>();
        List<string> names = new List<string>();

        ViewBag.Beschrijving = "Hier beoordeel je je medestudenten";
        ViewBag.Actie = "geef";

        foreach (Student student in students)
        {
            if (student.Id != getSessionStudentId())
            {
                evaluaties = evaluationservice.GetEvaluations(projectPeriodId, getSessionStudentId(), student.Id).ToList();
                evaluationList.Add(evaluaties);
                names.Add(student.Name);
            }
        }

        ViewBag.names = names;


        foreach (List<Evaluation> evaluation in evaluationList)
        {
            foreach (Evaluation evalInner in evaluation)
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
            EvaluationService evaluationservice = new EvaluationService();
            evaluationservice.Evaluate(model);
            return RedirectToAction("ProjectOverzicht");
        }
    }

    public ActionResult VoortgangInzien(int studentId)
    {
        EvaluationService evaluationservice = new EvaluationService();
        Dictionary<string, List<Evaluation>> projectsAvgEvaluations = evaluationservice.GetAvgEvaluations(studentId);
        ViewBag.projectNames = projectsAvgEvaluations.Keys.ToList();
        return View(projectsAvgEvaluations);
    }
}

