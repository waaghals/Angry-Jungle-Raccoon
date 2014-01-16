using System;
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

            using (var db = new Context())
            {
                foreach (Project p in db.Projects)
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
                Skill skill1 = new Skill() { Category = "Technisch ontwerp kennis" };
                Skill skill2 = new Skill() { Category = "Meepraten" };
                Skill skill3 = new Skill() { Category = "Plannen" };
                Skill skill4 = new Skill() { Category = "Testplan kennis" };

                Student henk = new Student() { Name = "Henk de Vries", Login = "hdvries" }; henk.RoleType.Add(RoleType.Student);
                Student klaas = new Student() { Name = "Klaas Achternaam", Login = "k_achternaam" }; henk.RoleType.Add(RoleType.Student);
                Student pieter = new Student() { Name = "Pieter Achternaam", Login = "p_achternaam" }; henk.RoleType.Add(RoleType.Student);
                Student joop = new Student() { Name = "Joop Achternaam", Login = "j_achternaam" }; henk.RoleType.Add(RoleType.Student);
                Student robin = new Student() {Name ="Robin Collard", Login = "r_collard"}; robin.RoleType.Add(RoleType.Student);

                User tutor = new User() { Name = "Dr Henk", Login = "drhenk1" }; tutor.RoleType.Add(RoleType.Teacher);
                tutor.MentorStudent.Add(henk);
                tutor.MentorStudent.Add(klaas);
                tutor.MentorStudent.Add(pieter);
                tutor.MentorStudent.Add(joop);
                tutor.MentorStudent.Add(robin);

                Group groep1 = new Group() { Name = "42in01soa", Tutor = tutor };
                groep1.Student.Add(henk);
                groep1.Student.Add(klaas);
                groep1.Student.Add(robin);
                Group groep2 = new Group() { Name = "42in01sob", Tutor = tutor };
                groep2.Student.Add(pieter);
                groep2.Student.Add(joop);

                //PROJ6
                List<Group> proj6groups = new List<Group>();
                proj6groups.Add(groep1);

                List<ProjectPeriod> proj6periods = new List<ProjectPeriod>();
                ProjectPeriod proj6period1 = new ProjectPeriod() { Name = "moment2", Start = new DateTime(2015, 01, 05), End = new DateTime(2015, 01, 10) };
                ProjectPeriod proj6period2 = new ProjectPeriod() { Name = "moment1", Start = new DateTime(2014, 12, 05), End = new DateTime(2014, 12, 10) };
                proj6periods.Add(proj6period1);
                proj6periods.Add(proj6period2);

                proj6period1.Evaluation.Add(new Evaluation() { Skill = skill1, By = henk, Grade = 60, For = klaas });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = skill1, By = klaas, Grade = 60, For = henk });
                proj6period1.Evaluation.Add(new Evaluation() {Skill=skill1,By=henk,Grade=60,For=robin});
                proj6period1.Evaluation.Add(new Evaluation() {Skill=skill1,By=robin,Grade=20,For=henk});
                proj6period1.Evaluation.Add(new Evaluation() {Skill=skill1,By=klaas,Grade=80,For=robin});
                proj6period1.Evaluation.Add(new Evaluation() {Skill=skill1,By=robin,Grade=90,For=klaas});


                proj6period1.Evaluation.Add(new Evaluation() { Skill = skill2, By = henk, Grade = 50, For = klaas });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = skill2, By = klaas, Grade = 70, For = henk });
                proj6period1.Evaluation.Add(new Evaluation() {Skill=skill2,By=henk,Grade=40,For=robin});
                proj6period1.Evaluation.Add(new Evaluation() {Skill=skill2,By=robin,Grade=20,For=henk});
                proj6period1.Evaluation.Add(new Evaluation() {Skill=skill2,By=klaas,Grade=30,For=robin});
                proj6period1.Evaluation.Add(new Evaluation() {Skill=skill2,By=robin,Grade=40,For=klaas});

                proj6period1.Evaluation.Add(new Evaluation() { Skill = skill3, By = henk, Grade = 10, For = klaas });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = skill3, By = klaas, Grade = 100, For = henk });
                proj6period1.Evaluation.Add(new Evaluation() {Skill=skill3,By=henk,Grade=80,For=robin});
                proj6period1.Evaluation.Add(new Evaluation() {Skill=skill3,By=robin,Grade=50,For=henk});
                proj6period1.Evaluation.Add(new Evaluation() {Skill=skill3,By=klaas,Grade=30,For=robin});
                proj6period1.Evaluation.Add(new Evaluation() {Skill=skill3,By=robin,Grade=100,For=klaas});

                Project PROJ6 = new Project()
                {
                    Name = "SOPROJ6",
                    Description = "Barometer/waterval ontwikkelen",
                    Anonymous = true,
                    Groups = proj6groups,
                    ProjectPeriod = proj6periods,
                    Skill = new List<Skill> { skill1, skill2, skill3 }
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
                proj5period1.Evaluation.Add(new Evaluation() { Skill = skill4, By = henk, Grade = 60, For = klaas });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = skill4, By = klaas, Grade = 60, For = henk });
                proj5period1.Evaluation.Add(new Evaluation() {Skill=skill4,By=henk,Grade=100,For=robin});
                proj5period1.Evaluation.Add(new Evaluation() {Skill=skill4,By=robin,Grade=90,For=henk});
                proj5period1.Evaluation.Add(new Evaluation() {Skill=skill4,By=klaas,Grade=80,For=robin});
                proj5period1.Evaluation.Add(new Evaluation() {Skill=skill4,By=robin,Grade=70,For=klaas});

                proj5period1.Evaluation.Add(new Evaluation() { Skill = skill2, By = henk, Grade = 50, For = klaas });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = skill2, By = klaas, Grade = 70, For = henk });
                proj5period1.Evaluation.Add(new Evaluation() {Skill=skill2,By=henk,Grade=60,For=robin});
                proj5period1.Evaluation.Add(new Evaluation() {Skill=skill2,By=robin,Grade=50,For=henk});
                proj5period1.Evaluation.Add(new Evaluation() {Skill=skill2,By=klaas,Grade=40,For=robin});
                proj5period1.Evaluation.Add(new Evaluation() {Skill=skill2,By=robin,Grade=30,For=klaas});


                proj5period1.Evaluation.Add(new Evaluation() { Skill = skill3, By = henk, Grade = 10, For = klaas });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = skill3, By = klaas, Grade = 100, For = henk });
                proj5period1.Evaluation.Add(new Evaluation() {Skill=skill3,By=henk,Grade=20,For=robin});
                proj5period1.Evaluation.Add(new Evaluation() {Skill=skill3,By=robin,Grade=10,For=henk});
                proj5period1.Evaluation.Add(new Evaluation() {Skill=skill3,By=klaas,Grade=20,For=robin});
                proj5period1.Evaluation.Add(new Evaluation() {Skill=skill3,By=robin,Grade=30,For=klaas});

                //GROEP 2
                proj5period1.Evaluation.Add(new Evaluation() { Skill = skill4, By = pieter, Grade = 60, For = joop });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = skill4, By = joop, Grade = 60, For = pieter });

                proj5period1.Evaluation.Add(new Evaluation() { Skill = skill2, By = pieter, Grade = 50, For = joop });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = skill2, By = joop, Grade = 70, For = pieter });

                proj5period1.Evaluation.Add(new Evaluation() { Skill = skill3, By = pieter, Grade = 10, For = joop });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = skill3, By = joop, Grade = 100, For = pieter });

                //PERIOD 2
                //GROEP 1
                proj5period2.Evaluation.Add(new Evaluation() { Skill = skill4, By = henk, Grade = 30, For = klaas });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = skill4, By = klaas, Grade = 70, For = henk });
                proj5period2.Evaluation.Add(new Evaluation() {Skill=skill4,By=henk,Grade=40,For=robin});
                proj5period2.Evaluation.Add(new Evaluation() {Skill=skill4,By=robin,Grade=50,For=henk});
                proj5period2.Evaluation.Add(new Evaluation() {Skill=skill4,By=klaas,Grade=60,For=robin});
                proj5period2.Evaluation.Add(new Evaluation() {Skill=skill4,By=robin,Grade=70,For=klaas});

                proj5period2.Evaluation.Add(new Evaluation() { Skill = skill2, By = henk, Grade = 10, For = klaas });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = skill2, By = klaas, Grade = 60, For = henk });
                proj5period2.Evaluation.Add(new Evaluation() {Skill=skill2,By=henk,Grade=70,For=robin});
                proj5period2.Evaluation.Add(new Evaluation() {Skill=skill2,By=robin,Grade=90,For=henk});
                proj5period2.Evaluation.Add(new Evaluation() {Skill=skill2,By=klaas,Grade=100,For=robin});
                proj5period2.Evaluation.Add(new Evaluation() {Skill=skill2,By=robin,Grade=90,For=klaas});

                proj5period2.Evaluation.Add(new Evaluation() { Skill = skill3, By = henk, Grade = 70, For = klaas });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = skill3, By = klaas, Grade = 40, For = henk });
                proj5period2.Evaluation.Add(new Evaluation() {Skill=skill3,By=henk,Grade=80,For=robin});
                proj5period2.Evaluation.Add(new Evaluation() {Skill=skill3,By=robin,Grade=70,For=henk});
                proj5period2.Evaluation.Add(new Evaluation() {Skill=skill3,By=klaas,Grade=60,For=robin});
                proj5period2.Evaluation.Add(new Evaluation() {Skill=skill3,By=robin,Grade=50,For=klaas});
                //GROEP 2
                proj5period2.Evaluation.Add(new Evaluation() { Skill = skill4, By = pieter, Grade = 80, For = joop });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = skill4, By = joop, Grade = 20, For = pieter });

                proj5period2.Evaluation.Add(new Evaluation() { Skill = skill2, By = pieter, Grade = 50, For = joop });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = skill2, By = joop, Grade = 70, For = pieter });

                proj5period2.Evaluation.Add(new Evaluation() { Skill = skill3, By = pieter, Grade = 90, For = joop });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = skill3, By = joop, Grade = 10, For = pieter });

                Project PROJ5 = new Project()
                {
                    Name = "SOPROJ5",
                    Description = "Barometer/waterval ontwerp",
                    Anonymous = true,
                    Groups = proj5groups,
                    ProjectPeriod = proj5periods,
                    Skill = new List<Skill> { skill4, skill2, skill3 }
                };



                ProjectRepository projrepo = new ProjectRepository(db);
                foreach(Project p in new List<Project>(){PROJ5, PROJ6})
                {
                    projrepo.Insert(p);
                }
                db.SaveChanges();
            }
        }
    }
}
