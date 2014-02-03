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
                if(g.Project != null)
                    if (g.Project.Equals(project) && !ret.Contains(g))
                        ret.Add(g);
            return ret;
        }

        public IEnumerable<Group> InProject(int ProjectId)
        {
            IList<Group> ret = new List<Group>();
            ret = new ProjectRepository(database).Get(ProjectId).Groups;
            return ret;
        }

        public IEnumerable<Group> NotInProject()
        {
            List<Group> ret = new List<Group>();
            Group d = table.First();
            foreach (Group groep in table.ToList())
                    if (groep.Project == null)
                        ret.Add(groep);
            return ret;
        }

        public Group ByName(string grpName)
        {
            Group grp = null;
            foreach (Group g in table)
            {
                System.Diagnostics.Debug.WriteLine("search string: " + grpName + ", groupname: " + g.Name);
                if (g.Name.Equals(grpName))
                {
                    grp = g;
                    break;
                }
            }
            return grp;
        }

    }
}
