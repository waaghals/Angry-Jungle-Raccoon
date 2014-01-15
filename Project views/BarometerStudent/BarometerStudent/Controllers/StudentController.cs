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
        foreach (Group group in student.Groups)
        {
            foreach (Project project in group.Project)
            {
                projectNames.Add(project.Name);
                evaluationList = (List<Evaluation>)project.GetAverageEvaluations(student);
                evaluationList = BubbleSort(evaluationList);
                projectsAvgEvaluations.Add(project.Name, evaluationList);
            }
        }

        ViewBag.projectNames = projectNames;
        return View(projectsAvgEvaluations);
    }

    private List<Evaluation> BubbleSort(List<Evaluation> evaluationList)
    {
        for (int outer = evaluationList.Count - 1; outer >= 1; outer--)
            for (int inner = 0; inner < outer; inner++) // inner loop (forward)
                if ((evaluationList[inner].CompareTo(evaluationList[inner + 1]) == -1))
                {
                    Evaluation temp = evaluationList[inner];
                    evaluationList[inner] = evaluationList[inner + 1];
                    evaluationList[inner + 1] = temp;
                }
        return evaluationList;
    }
}

