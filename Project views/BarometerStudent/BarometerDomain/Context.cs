using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarometerDomain
{
    class Context  :DbContext
    {
        public Context()
            : base("AngryJungleRaccoon")
        {

        }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectPeriod> ProjectPeriods { get; set; }
        public DbSet<ScoreType> ScoreTypes { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
