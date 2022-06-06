using System;
using System.Collections.Generic;
//using
//System.Data.Entity;
//using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
///using EMKAN.Entity.ModelDto;
using Microsoft.EntityFrameworkCore;
namespace DataAccess.DbContexts
{
    public class AppDbContext : DbContext
    {
    //    public AppDbContext() { }



        public AppDbContext(DbContextOptions<AppDbContext> connString) : base(connString)
        {
        }
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<ToDoTask> ToDoTasks { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<AuditTrail> AuditTrails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder OptionsBuilder)
        {
            if (!OptionsBuilder.IsConfigured)
            {
                OptionsBuilder.UseSqlServer("DB");
            }
        }
    }

}
