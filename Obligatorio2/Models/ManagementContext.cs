using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Obligatorio2.Models
{
    public class ManagementContext : DbContext
    {
        public ManagementContext() : base("MyConn") { }

        public DbSet<AppGroup> AppGroup { get; set; }
        public DbSet<AppProcedure> AppProcedure { get; set; }
        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<Case> Case { get; set; }
        public DbSet<Requester> Requester { get; set; }
        public DbSet<Stage> Stage { get; set; }

    }
}