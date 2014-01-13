using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarometerDomain.Model;

namespace BarometerDomain.Repositories
{
    public class GroupRepository : GenericRepository<Group>
    {
        public GroupRepository(Context c) 
            : base(c) 
        {
            table = database.Groups;
        }

        public IEnumerable<Group> ByStudent(Student student)
        {
            List<Group> ret = new List<Group>();
            foreach (Group g in table)
                if (g.Student.Contains(student) && !ret.Contains(g))
                    ret.Add(g);
            return ret;
        }

        public IEnumerable<Group> ByStudent(int StudentId)
        {
            List<Group> ret = new List<Group>();
            foreach (Group g in table)
                foreach (Student s in g.Student)
                    if (s.Id == StudentId && !ret.Contains(g))
                        ret.Add(g);
            return ret;
        }

        public IEnumerable<Group> InProject(Project project)
        {
            List<Group> ret = new List<Group>();
            foreach (Group g in table)
                if (g.Project.Contains(project) && !ret.Contains(g))
                    ret.Add(g);
            return ret;
        }

        public IEnumerable<Group> InProject(int ProjectId)
        {
            List<Group> ret = new List<Group>();
            foreach (Group g in table)
                foreach (Project p in g.Project)
                    if (p.Id == ProjectId&& !ret.Contains(g))
                        ret.Add(g);
            return ret;
        }

    }
}
