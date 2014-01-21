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

        public void Evaluate(List<List<Evaluation>> evaluations)
        {
            Context context = new Context();
                List<Evaluation> update = new List<Evaluation>();
                foreach (List<Evaluation> l in evaluations)
                    foreach (Evaluation e in l)
                    {
                        // bij het invoegen keer 10
                        e.Grade *= 10;
                        update.Add(e);
                    }

                SkillRepository sr = new SkillRepository(context);
                StudentRepository str = new StudentRepository(context);
                EvaluationRepository er = new EvaluationRepository(context);

                foreach (Evaluation e in update)
                {
                    e.Skill = sr.Get(e.Skill.Id);
                    e.By = str.Get(e.By.Id);
                    e.For = str.Get(e.For.Id);
                    er.Update(e);
                }

                er.Save();
                sr.Save();
                str.Save();
        }

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

        public ICollection<Evaluation> GetEvaluations(int projectPeriodId, int byStudent, int forStudent)
        {
            List<Evaluation> ret = new List<Evaluation>();
            ProjectPeriodRepository ppr = new ProjectPeriodRepository(new Context());
            ProjectPeriod period = ppr.Get(projectPeriodId);

            foreach (Evaluation eval in period.Evaluation)
            {
                if (eval.By.Id == byStudent && eval.For.Id == forStudent)
                    ret.Add(eval);
            }
            return ret;
        }
    }
}