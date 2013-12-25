using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BarometerDomain
{
    class RunDBTest
    {

        static void Main(string[] args)
        {
            Context context = new Context();
            User user = new User() { Name = "test" };
            context.Users.Add(user);

            using(var db = new Context())
            {
                db.Skills.Add(new Skill() { Category = "test skill" });
                db.SaveChanges();

                foreach(Skill s in db.Skills)
                    Console.WriteLine(s.Id + ", " + s.Category);
            }
            Console.ReadLine();
        }
    }
}
