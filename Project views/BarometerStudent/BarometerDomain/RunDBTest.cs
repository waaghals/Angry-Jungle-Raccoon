﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BarometerDomain.Model;
using BarometerDomain.Repositories;

namespace BarometerDomain
{
    public class RunDBTest
    {

        static void Main(string[] args)
        {
            RunDBTest.fillScript();
            using(var db = new Context())
            {
                    foreach(Project p in db.Projects)
                        Console.WriteLine(p.Id + "," + p.Name + "," + p.Description + "," + p.Anonymous);
                    foreach (Student s in db.Students)
                        Console.WriteLine(s.Id + "," + s.Name);
            }
            Console.ReadLine();
        }

        public static void fillScript()
        {
            using (var db = new Context())
            {
                //algemeen
                Skill skill1 = new Skill() { Category = "technisch ontwerp kennis" };
                Skill skill2 = new Skill() { Category = "meepraten" };
                Skill skill3 = new Skill() { Category = "plannen" };
                Skill skill4 = new Skill() { Category = "testplan kennis" };
                SkillRepository skillrepo = new SkillRepository(db);
                skillrepo.Insert(skill1);
                skillrepo.Insert(skill2);
                skillrepo.Insert(skill3);
                skillrepo.Insert(skill4);

                Student henk = new Student() { Name = "henk", Login = "hdvries" }; henk.RoleType.Add(RoleType.Student);
                Student klaas = new Student() { Name = "klaas", Login = "k_achternaam" }; henk.RoleType.Add(RoleType.Student);
                Student pieter = new Student() { Name = "pieter", Login = "p_achternaam" }; henk.RoleType.Add(RoleType.Student);
                Student joop = new Student() { Name = "joop", Login = "j_achternaam" }; henk.RoleType.Add(RoleType.Student);
                StudentRepository studentrepo = new StudentRepository(db);
                studentrepo.Insert(henk);
                studentrepo.Insert(klaas);
                studentrepo.Insert(pieter);
                studentrepo.Insert(joop);

                User tutor = new User() { Name = "dr henk", Login = "drhenk1" }; tutor.RoleType.Add(RoleType.Teacher);
                UserRepository userrepo = new UserRepository(db);
                userrepo.Insert(tutor);
                Group groep1 = new Group() { Name = "42in01soa", Tutor = tutor };
                groep1.Student.Add(henk);
                groep1.Student.Add(klaas);
                Group groep2 = new Group() { Name = "42in01sob", Tutor = tutor };
                groep2.Student.Add(pieter);
                groep2.Student.Add(joop);
                GroupRepository grouprepo = new GroupRepository(db);
                grouprepo.Insert(groep1);
                grouprepo.Insert(groep2);

                //PROJ6
                List<Group> proj6groups = new List<Group>();
                proj6groups.Add(groep1);

                List<ProjectPeriod> proj6periods = new List<ProjectPeriod>();
                ProjectPeriod proj6period1 = new ProjectPeriod() { Name = "moment2", Start = new DateTime(2015, 01, 05), End = new DateTime(2015, 01, 10) };
                ProjectPeriod proj6period2 = new ProjectPeriod() { Name = "moment1", Start = new DateTime(2014, 12, 05), End = new DateTime(2014, 12, 10) };
                proj6periods.Add(proj6period1);
                proj6periods.Add(proj6period2);

                proj6period1.Evaluation.Add(new Evaluation() { Skill = skill1, By = henk, Grade = 6, For = klaas });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = skill1, By = klaas, Grade = 6, For = henk });

                proj6period1.Evaluation.Add(new Evaluation() { Skill = skill2, By = henk, Grade = 5, For = klaas });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = skill2, By = klaas, Grade = 7, For = henk });

                proj6period1.Evaluation.Add(new Evaluation() { Skill = skill3, By = henk, Grade = 1, For = klaas });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = skill3, By = klaas, Grade = 10, For = henk });

                Project PROJ6 = new Project()
                {
                    Name = "SOPROJ6",
                    Description = "Barometer/waterval ontwikkelen",
                    Anonymous = true,
                    Groups = proj6groups,
                    ProjectPeriod = proj6periods
                };

                //PROJ5

                List<Group> proj5groups = new List<Group>();
                proj5groups.Add(groep1);
                proj5groups.Add(groep2);

                List<ProjectPeriod> proj5periods = new List<ProjectPeriod>();
                ProjectPeriod proj5period1 = new ProjectPeriod() { Name = "moment12", Start = new DateTime(2014, 01, 05), End = new DateTime(2014, 01, 10) };
                ProjectPeriod proj5period2 = new ProjectPeriod() { Name = "moment11", Start = new DateTime(2013, 12, 05), End = new DateTime(2013, 12, 10) };
                proj5periods.Add(proj5period1);
                proj5periods.Add(proj5period2);

                //PERIOD 1
                //GROEP 1
                proj5period1.Evaluation.Add(new Evaluation() { Skill = skill4, By = henk, Grade = 6, For = klaas });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = skill4, By = klaas, Grade = 6, For = henk });

                proj5period1.Evaluation.Add(new Evaluation() { Skill = skill2, By = henk, Grade = 5, For = klaas });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = skill2, By = klaas, Grade = 7, For = henk });

                proj5period1.Evaluation.Add(new Evaluation() { Skill = skill3, By = henk, Grade = 1, For = klaas });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = skill3, By = klaas, Grade = 10, For = henk });
                //GROEP 2
                proj5period1.Evaluation.Add(new Evaluation() { Skill = skill4, By = pieter, Grade = 6, For = joop });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = skill4, By = joop, Grade = 6, For = pieter });

                proj5period1.Evaluation.Add(new Evaluation() { Skill = skill2, By = pieter, Grade = 5, For = joop });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = skill2, By = joop, Grade = 7, For = pieter });

                proj5period1.Evaluation.Add(new Evaluation() { Skill = skill3, By = pieter, Grade = 1, For = joop });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = skill3, By = joop, Grade = 10, For = pieter });

                //PERIOD 2
                //GROEP 1
                proj5period2.Evaluation.Add(new Evaluation() { Skill = skill4, By = henk, Grade = 3, For = klaas });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = skill4, By = klaas, Grade = 7, For = henk });

                proj5period2.Evaluation.Add(new Evaluation() { Skill = skill2, By = henk, Grade = 1, For = klaas });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = skill2, By = klaas, Grade = 6, For = henk });

                proj5period2.Evaluation.Add(new Evaluation() { Skill = skill3, By = henk, Grade = 7, For = klaas });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = skill3, By = klaas, Grade = 4, For = henk });
                //GROEP 2
                proj5period2.Evaluation.Add(new Evaluation() { Skill = skill4, By = pieter, Grade = 8, For = joop });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = skill4, By = joop, Grade = 2, For = pieter });

                proj5period2.Evaluation.Add(new Evaluation() { Skill = skill2, By = pieter, Grade = 5, For = joop });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = skill2, By = joop, Grade = 7, For = pieter });

                proj5period2.Evaluation.Add(new Evaluation() { Skill = skill3, By = pieter, Grade = 9, For = joop });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = skill3, By = joop, Grade = 1, For = pieter });

                Project PROJ5 = new Project()
                {
                    Name = "SOPROJ5",
                    Description = "Barometer/waterval ontwerp",
                    Anonymous = true,
                    Groups = proj5groups,
                    ProjectPeriod = proj5periods
                };


                ProjectRepository projrepo = new ProjectRepository(db);
                ProjectPeriodRepository projperiodrepo = new ProjectPeriodRepository(db);
                EvaluationRepository evalrepo = new EvaluationRepository(db);
                foreach(Project p in new List<Project>(){PROJ5, PROJ6})
                {
                    foreach (Group gr in p.Groups)
                        foreach (Student s in gr.Student)
                            p.Students.Add(s);

                    projrepo.Insert(p);
                    foreach(ProjectPeriod pp in p.ProjectPeriod)
                    {
                        projperiodrepo.Insert(pp);
                        foreach(Evaluation e in pp.Evaluation)
                        {
                            evalrepo.Insert(e);
                        }
                    }
                }
            }
        }
    }
}
