using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarometerDomain
{
    class RunDBTest
    {

        static void Main(string[] args)
        {
            Context context = new Context();
            User user = new User() { Name = "test" };
            context.Users.Add(user);
            Console.ReadLine();

        }
    }
}
