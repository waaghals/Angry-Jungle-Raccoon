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

            /*using (var db = new Context())
            {
                foreach (Project p in db.Projects)
                    Console.WriteLine(p.Id + "," + p.Name + "," + p.Description + "," + p.Anonymous);
                foreach (Student s in db.Students)
                    Console.WriteLine(s.Id + "," + s.Name);
            }*/

            Console.ReadLine();
        }

        public static void fillScript()
        {
            using (var db = new Context())
            {
                //algemeen
                Skill technischontwerp = new Skill() { Category = "Technisch ontwerp kennis" };
                Skill meepraten = new Skill() { Category = "Meepraten" };
                Skill plannen = new Skill() { Category = "Plannen" };
                Skill testplankennis = new Skill() { Category = "Testplan kennis" };

                Student Edwin = new Student() { Name = "Edwin Hattink", Login = "egjhatti", Number = 2063703 }; 
                Edwin.RoleType = "Student";
                Student Yannik = new Student() { Name = "Yannik Hegge", Login = "yahegge", Number = 2066492};
                Yannik.RoleType = "Student";
                Student Yorick = new Student() { Name = "Yorick Klinken", Login = "ymklinken" , Number = 2063061};
                Yorick.RoleType = "Student";
                Student Michael = new Student() { Name = "Michael van de Ven", Login = "mven7" , Number = 2061033};
                Michael.RoleType = "Student";
                Student Jip = new Student() {Name ="Jip Verhoeven", Login = "jverhoev5", Number = 2062172};
                Jip.RoleType = "Student";

                User Robin = new User() { Name = "Robin Collard", Login = "rcollard" }; Robin.RoleType = "Docent";
                Robin.MentorStudent.Add(Edwin);
                Robin.MentorStudent.Add(Yannik);
                Robin.MentorStudent.Add(Yorick);
                Robin.MentorStudent.Add(Michael);
                Robin.MentorStudent.Add(Jip);

                Group groep1 = new Group() { Name = "42in01soa", Tutor = Robin };
                groep1.Student.Add(Edwin);
                groep1.Student.Add(Yannik);
                groep1.Student.Add(Yorick);
                groep1.Student.Add(Michael);
                groep1.Student.Add(Jip);
                Group groep2 = new Group() { Name = "42in02soc", Tutor = Robin };
                groep2.Student.Add(Edwin);
                groep2.Student.Add(Yannik);
                groep2.Student.Add(Yorick);
                groep2.Student.Add(Michael);
                groep2.Student.Add(Jip);

                //PROJ6
                List<Group> proj6groups = new List<Group>();
                proj6groups.Add(groep2);

                List<ProjectPeriod> proj6periods = new List<ProjectPeriod>();
                ProjectPeriod proj6period1 = new ProjectPeriod() { Name = "moment1", Start = new DateTime(2015, 01, 05), End = new DateTime(2015, 01, 10) };
                ProjectPeriod proj6period2 = new ProjectPeriod() { Name = "moment2", Start = new DateTime(2014, 12, 05), End = new DateTime(2014, 12, 10) };
                proj6periods.Add(proj6period1);
                proj6periods.Add(proj6period2);
                //period1
                proj6period1.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Edwin, Grade = 100, For = Yannik });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Edwin, Grade = 90, For = Yorick });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Edwin, Grade = 80, For = Michael });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Edwin, Grade = 70, For = Jip });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Yannik, Grade = 60, For = Edwin });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Yannik, Grade = 50, For = Yorick });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Yannik, Grade = 40, For = Michael });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Yannik, Grade = 30, For = Jip });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Jip, Grade = 20, For = Edwin });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Jip, Grade = 10, For = Yannik });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Jip, Grade = 20, For = Yorick });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Jip, Grade = 30, For = Michael });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Yorick, Grade = 40, For = Edwin });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Yorick, Grade = 50, For = Jip });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Yorick, Grade = 60, For = Yannik });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Yorick, Grade = 70, For = Michael });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Michael, Grade = 80, For = Edwin });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Michael, Grade = 90, For = Yannik });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Michael, Grade = 100, For = Yorick });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Michael, Grade = 90, For = Jip });

                proj6period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Edwin, Grade = 80, For = Yannik });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Edwin, Grade = 70, For = Yorick });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Edwin, Grade = 60, For = Michael });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Edwin, Grade = 50, For = Jip });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Yannik, Grade = 40, For = Edwin });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Yannik, Grade = 30, For = Yorick });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Yannik, Grade = 20, For = Michael });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Yannik, Grade = 10, For = Jip });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Jip, Grade = 100, For = Edwin });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Jip, Grade = 90, For = Yannik });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Jip, Grade = 80, For = Yorick });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Jip, Grade = 70, For = Michael });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Yorick, Grade = 60, For = Edwin });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Yorick, Grade = 50, For = Jip });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Yorick, Grade = 40, For = Yannik });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Yorick, Grade = 30, For = Michael });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Michael, Grade = 20, For = Edwin });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Michael, Grade = 10, For = Yannik });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Michael, Grade = 20, For = Yorick });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Michael, Grade = 30, For = Jip });

                proj6period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Edwin, Grade = 40, For = Yannik });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Edwin, Grade = 50, For = Yorick });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Edwin, Grade = 60, For = Michael });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Edwin, Grade = 70, For = Jip });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Yannik, Grade = 80, For = Edwin });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Yannik, Grade = 90, For = Yorick });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Yannik, Grade = 100, For = Michael });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Yannik, Grade = 90, For = Jip });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Jip, Grade = 80, For = Edwin });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Jip, Grade = 70, For = Yannik });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Jip, Grade = 60, For = Yorick });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Jip, Grade = 50, For = Michael });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Yorick, Grade = 40, For = Edwin });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Yorick, Grade = 30, For = Jip });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Yorick, Grade = 20, For = Yannik });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Yorick, Grade = 10, For = Michael });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Michael, Grade = 20, For = Edwin });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Michael, Grade = 30, For = Yannik });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Michael, Grade = 40, For = Yorick });
                proj6period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Michael, Grade = 50, For = Jip });
                //period2
                proj6period2.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Edwin, Grade = 60, For = Yannik });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Edwin, Grade = 70, For = Yorick });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Edwin, Grade = 80, For = Michael });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Edwin, Grade = 90, For = Jip });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Yannik, Grade = 100, For = Edwin });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Yannik, Grade = 90, For = Yorick });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Yannik, Grade = 80, For = Michael });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Yannik, Grade = 70, For = Jip });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Jip, Grade = 60, For = Edwin });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Jip, Grade = 50, For = Yannik });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Jip, Grade = 40, For = Yorick });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Jip, Grade = 30, For = Michael });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Yorick, Grade = 20, For = Edwin });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Yorick, Grade = 10, For = Jip });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Yorick, Grade = 20, For = Yannik });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Yorick, Grade = 30, For = Michael });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Michael, Grade = 40, For = Edwin });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Michael, Grade = 50, For = Yannik });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Michael, Grade = 60, For = Yorick });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = technischontwerp, By = Michael, Grade = 70, For = Jip });

                proj6period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Edwin, Grade = 80, For = Yannik });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Edwin, Grade = 90, For = Yorick });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Edwin, Grade = 100, For = Michael });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Edwin, Grade = 90, For = Jip });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Yannik, Grade = 80, For = Edwin });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Yannik, Grade = 70, For = Yorick });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Yannik, Grade = 60, For = Michael });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Yannik, Grade = 50, For = Jip });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Jip, Grade = 40, For = Edwin });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Jip, Grade = 30, For = Yannik });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Jip, Grade = 20, For = Yorick });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Jip, Grade = 10, For = Michael });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Yorick, Grade = 20, For = Edwin });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Yorick, Grade = 30, For = Jip });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Yorick, Grade = 40, For = Yannik });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Yorick, Grade = 50, For = Michael });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Michael, Grade = 60, For = Edwin });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Michael, Grade = 70, For = Yannik });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Michael, Grade = 80, For = Yorick });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Michael, Grade = 90, For = Jip });

                proj6period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Edwin, Grade = 100, For = Yannik });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Edwin, Grade = 90, For = Yorick });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Edwin, Grade = 80, For = Michael });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Edwin, Grade = 70, For = Jip });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Yannik, Grade = 60, For = Edwin });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Yannik, Grade = 50, For = Yorick });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Yannik, Grade = 40, For = Michael });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Yannik, Grade = 30, For = Jip });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Jip, Grade = 20,For = Edwin });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Jip, Grade = 10, For = Yannik });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Jip, Grade = 20, For = Yorick });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Jip, Grade = 30, For = Michael });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Yorick, Grade = 40, For = Edwin });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Yorick, Grade = 50, For = Jip });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Yorick, Grade = 60, For = Yannik });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Yorick, Grade = 70, For = Michael });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Michael, Grade = 80, For = Edwin });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Michael, Grade = 90, For = Yannik });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Michael, Grade = 100, For = Yorick });
                proj6period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Michael, Grade = 90, For = Jip });

                Project PROJ6 = new Project()
                {
                    Name = "SOPROJ6",
                    Description = "Barometer/waterval ontwikkelen",
                    Anonymous = true,
                    Groups = proj6groups,
                    ProjectPeriod = proj6periods,
                    Skill = new List<Skill> { technischontwerp, meepraten, plannen }
                };

                //PROJ5

                List<Group> proj5groups = new List<Group>();
                proj5groups.Add(groep1);

                List<ProjectPeriod> proj5periods = new List<ProjectPeriod>();
                ProjectPeriod proj5period1 = new ProjectPeriod() { Name = "moment11", Start = new DateTime(2014, 01, 05), End = new DateTime(2014, 01, 10) };
                ProjectPeriod proj5period2 = new ProjectPeriod() { Name = "moment12", Start = new DateTime(2013, 12, 05), End = new DateTime(2013, 12, 10) };
                proj5periods.Add(proj5period1);
                proj5periods.Add(proj5period2);

                proj5period1.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Edwin, Grade = 100, For = Yannik });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Edwin, Grade = 90, For = Yorick });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Edwin, Grade = 80, For = Michael });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Edwin, Grade = 70, For = Jip });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Yannik, Grade = 60, For = Edwin });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Yannik, Grade = 50, For = Yorick });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Yannik, Grade = 40, For = Michael });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Yannik, Grade = 30, For = Jip });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Jip, Grade = 20, For = Edwin });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Jip, Grade = 10, For = Yannik });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Jip, Grade = 20, For = Yorick });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Jip, Grade = 30, For = Michael });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Yorick, Grade = 40, For = Edwin });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Yorick, Grade = 50, For = Jip });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Yorick, Grade = 60, For = Yannik });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Yorick, Grade = 70, For = Michael });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Michael, Grade = 80, For = Edwin });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Michael, Grade = 90, For = Yannik });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Michael, Grade = 100, For = Yorick });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Michael, Grade = 90, For = Jip });

                proj5period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Edwin, Grade = 80, For = Yannik });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Edwin, Grade = 70, For = Yorick });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Edwin, Grade = 60, For = Michael });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Edwin, Grade = 50, For = Jip });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Yannik, Grade = 40, For = Edwin });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Yannik, Grade = 30, For = Yorick });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Yannik, Grade = 20, For = Michael });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Yannik, Grade = 10, For = Jip });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Jip, Grade = 100, For = Edwin });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Jip, Grade = 90, For = Yannik });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Jip, Grade = 80, For = Yorick });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Jip, Grade = 70, For = Michael });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Yorick, Grade = 60, For = Edwin });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Yorick, Grade = 50, For = Jip });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Yorick, Grade = 40, For = Yannik });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Yorick, Grade = 30, For = Michael });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Michael, Grade = 20, For = Edwin });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Michael, Grade = 10, For = Yannik });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Michael, Grade = 20, For = Yorick });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Michael, Grade = 30, For = Jip });

                proj5period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Edwin, Grade = 40, For = Yannik });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Edwin, Grade = 50, For = Yorick });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Edwin, Grade = 60, For = Michael });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Edwin, Grade = 70, For = Jip });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Yannik, Grade = 80, For = Edwin });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Yannik, Grade = 90, For = Yorick });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Yannik, Grade = 100, For = Michael });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Yannik, Grade = 90, For = Jip });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Jip, Grade = 80, For = Edwin });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Jip, Grade = 70, For = Yannik });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Jip, Grade = 60, For = Yorick });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Jip, Grade = 50, For = Michael });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Yorick, Grade = 40, For = Edwin });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Yorick, Grade = 30, For = Jip });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Yorick, Grade = 20, For = Yannik });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Yorick, Grade = 10, For = Michael });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Michael, Grade = 20, For = Edwin });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Michael, Grade = 30, For = Yannik });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Michael, Grade = 40, For = Yorick });
                proj5period1.Evaluation.Add(new Evaluation() { Skill = plannen, By = Michael, Grade = 50, For = Jip });
                //period2
                proj5period2.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Edwin, Grade = 60, For = Yannik });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Edwin, Grade = 70, For = Yorick });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Edwin, Grade = 80, For = Michael });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Edwin, Grade = 90, For = Jip });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Yannik, Grade = 100, For = Edwin });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Yannik, Grade = 90, For = Yorick });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Yannik, Grade = 80, For = Michael });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Yannik, Grade = 70, For = Jip });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Jip, Grade = 60, For = Edwin });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Jip, Grade = 50, For = Yannik });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Jip, Grade = 40, For = Yorick });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Jip, Grade = 30, For = Michael });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Yorick, Grade = 20, For = Edwin });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Yorick, Grade = 10, For = Jip });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Yorick, Grade = 20, For = Yannik });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Yorick, Grade = 30, For = Michael });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Michael, Grade = 40, For = Edwin });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Michael, Grade = 50, For = Yannik });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Michael, Grade = 60, For = Yorick });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = testplankennis, By = Michael, Grade = 70, For = Jip });

                proj5period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Edwin, Grade = 80, For = Yannik });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Edwin, Grade = 90, For = Yorick });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Edwin, Grade = 100, For = Michael });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Edwin, Grade = 90, For = Jip });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Yannik, Grade = 80, For = Edwin });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Yannik, Grade = 70, For = Yorick });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Yannik, Grade = 60, For = Michael });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Yannik, Grade = 50, For = Jip });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Jip, Grade = 40, For = Edwin });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Jip, Grade = 30, For = Yannik });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Jip, Grade = 20, For = Yorick });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Jip, Grade = 10, For = Michael });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Yorick, Grade = 20, For = Edwin });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Yorick, Grade = 30, For = Jip });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Yorick, Grade = 40, For = Yannik });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Yorick, Grade = 50, For = Michael });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Michael, Grade = 60, For = Edwin });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Michael, Grade = 70, For = Yannik });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Michael, Grade = 80, For = Yorick });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = meepraten, By = Michael, Grade = 90, For = Jip });

                proj5period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Edwin, Grade = 100, For = Yannik });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Edwin, Grade = 90, For = Yorick });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Edwin, Grade = 80, For = Michael });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Edwin, Grade = 70, For = Jip });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Yannik, Grade = 60, For = Edwin });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Yannik, Grade = 50, For = Yorick });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Yannik, Grade = 40, For = Michael });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Yannik, Grade = 30, For = Jip });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Jip, Grade = 20, For = Edwin });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Jip, Grade = 10, For = Yannik });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Jip, Grade = 20, For = Yorick });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Jip, Grade = 30, For = Michael });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Yorick, Grade = 40, For = Edwin });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Yorick, Grade = 50, For = Jip });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Yorick, Grade = 60, For = Yannik });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Yorick, Grade = 70, For = Michael });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Michael, Grade = 80, For = Edwin });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Michael, Grade = 90, For = Yannik });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Michael, Grade = 100, For = Yorick });
                proj5period2.Evaluation.Add(new Evaluation() { Skill = plannen, By = Michael, Grade = 90, For = Jip });
   

                Project PROJ5 = new Project()
                {
                    Name = "SOPROJ5",
                    Description = "Barometer/waterval ontwerp",
                    Anonymous = true,
                    Groups = proj5groups,
                    ProjectPeriod = proj5periods,
                    Skill = new List<Skill> { testplankennis, meepraten, plannen }
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
