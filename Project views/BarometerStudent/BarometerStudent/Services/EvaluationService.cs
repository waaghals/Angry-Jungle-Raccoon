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
        Context context;
        public EvaluationService()
        {
            context = new Context();
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

        private List<Evaluation> BubbleSortPeriod(List<Evaluation> evaluationList)
        {
            for (int outer = evaluationList.Count - 1; outer >= 1; outer--)
                for (int inner = 0; inner < outer; inner++) // inner loop (forward)
                    if ((evaluationList[inner].CompareToWithPeriod(evaluationList[inner + 1]) == -1))
                    {
                        Evaluation temp = evaluationList[inner];
                        evaluationList[inner] = evaluationList[inner + 1];
                        evaluationList[inner + 1] = temp;
                    }
            return evaluationList;
        }

        public void Evaluate(List<List<Evaluation>> evaluations)
        {
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
            StudentRepository studentRepo = new StudentRepository(context);
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
            ProjectPeriodRepository ppr = new ProjectPeriodRepository(context);
            ProjectPeriod period = ppr.Get(projectPeriodId);

            foreach (Evaluation eval in period.Evaluation)
            {
                if (eval.By.Id == byStudent && eval.For.Id == forStudent)
                    ret.Add(eval);
            }
            return ret;
        }

        public Dictionary<string, List<Evaluation>> GetGroupEvaluation(int groepId)
        {
            GroupRepository gr = new GroupRepository(context);
            Group group = gr.Get(groepId);
            Project project = group.Project;

            List<ProjectPeriod> projectPeriodList = new List<ProjectPeriod>();

            foreach (ProjectPeriod period in group.Project.ProjectPeriod)
            {
                if (group.Project.Id == project.Id)
                {
                    projectPeriodList.Add(period);
                }
            }
            Dictionary<string, List<Evaluation>> evalutionsPerForStudent = new Dictionary<string, List<Evaluation>>();

            foreach (Student student in group.Student)
            {
                List<Evaluation> sortedList = BubbleSortPeriod((List<Evaluation>)project.GetEvaluations(student));
                evalutionsPerForStudent.Add(student.Name, sortedList);
            }

            return evalutionsPerForStudent;
        }
    }
}