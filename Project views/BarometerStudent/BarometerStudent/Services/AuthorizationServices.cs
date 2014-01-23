using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BarometerDomain;
using BarometerDomain.Model;
using BarometerDomain.Repositories;

namespace BarometerStudent.Services
{
    public class AuthorizationServices
    {
        //
        // GET: /AuthorizationServices/

        private User user;

        public AuthorizationServices(User newUser)
        {
            user = newUser;
        }

        /*Docent---------------------*/
        public bool IsDocent()
        {
            if (user.RoleType.Equals("Docent"))
            { 
                return true; 
            }
            return false;
        }


        public bool ShowMentorStudent(int newStudentenId) 
        {
            if (LoggedIn()) 
            {
                foreach (Student student in user.MentorStudent)
                {
                    if (student.Id == newStudentenId)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /*End Docent---------------------*/

        private Boolean LoggedIn() 
        {
            if (user != null)
            {
                return true;
            }
            return false;
        }
    }
}
