using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BarometerDomain.Model;
using BarometerDomain.Repositories;
using BarometerDomain;

namespace BarometerStudent.Services
{
    public class UserService
    {
        public User Login(int studentNumber, string login)
        {
            User user = null;
            Context con = new Context();
            StudentRepository sr = new StudentRepository(con);
            UserRepository ur = new UserRepository(con);
            user = sr.ByNumber(studentNumber);
            if (user != null)
            {
                if (user.Login == null)
                {
                    user.Login = login;
                }
                return user;
            }else{
                user = ur.ByLogin(login);
            }
            return user;
        }
    }
}