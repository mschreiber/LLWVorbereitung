using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTimes.model
{
    class ProjectsContext: DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public ProjectsContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);
            // connect to sqlite database
            options.UseSqlite(@"Data Source=C:\Users\User\Documents\GitHub\LLWVorbereitung\ProjectTime\Datenbank\projectTimes.db;");
        }
    }
}
