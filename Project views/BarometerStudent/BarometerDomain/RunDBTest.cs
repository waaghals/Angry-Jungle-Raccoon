using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BarometerDomain.Model;

namespace BarometerDomain
{
    class RunDBTest
    {

        static void Main(string[] args)
        {
            using(var db = new Context())
            {
                Project proj6 =
                    new Project()
                    {
                        Id = 06,
                        Name = "waterval",
                        Description = "In dit project zal je leren hoe ver je kan gaan in het gebruiken van het woord \"Dynamisch process\" als smoes",
                        Anonymous = true
                    };
                db.Projects.Add(proj6);
                db.SaveChanges();

                foreach(Project p in db.Projects)
                    Console.WriteLine(p.Id + "," + p.Name + "," + p.Description + "," + p.Anonymous);
            }
            Console.ReadLine();
        }
    }
}
