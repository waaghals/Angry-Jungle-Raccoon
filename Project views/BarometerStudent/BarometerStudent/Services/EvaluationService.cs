using BarometerDomain;
using BarometerDomain.Model;
using BarometerDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BarometerStudent.Services
{
    public class EvaluationService
    {

        public Dictionary<string, List<Evaluation>> GetAvgEvaluations(int studentId)
        {
            StudentRepository studentRepo = new StudentRepository(new Context());
            Student student = studentRepo.Get(studentId);

            List<Evaluation> evaluationList = new List<Evaluation>();
            Dictionary<string, List<Evaluation>> projectsAvgEvaluations = new Dictionary<string, List<Evaluation>>();
            foreach (Group group in student.Groups)
            {
                evaluationList = group.Project.GetAverageEvaluations(student).ToList();
                evaluationList = BubbleSort(evaluationList);
                projectsAvgEvaluations.Add(group.Project.Name, evaluationList);
            }

            return projectsAvgEvaluations;
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
}