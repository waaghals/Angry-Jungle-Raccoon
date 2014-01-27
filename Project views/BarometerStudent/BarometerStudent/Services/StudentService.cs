﻿using BarometerDomain;
using BarometerDomain.Model;
using BarometerDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BarometerStudent.Services
{
    public class StudentService
    {
        Context context;

        public StudentService()
        {
            context = new Context();
        }

        public List<Student> GetMentorStudents(int id)
        {
            UserRepository userrep = new UserRepository(context);
            User user = userrep.Get(id);
            return user.MentorStudent.ToList<Student>();
        }
    }


}