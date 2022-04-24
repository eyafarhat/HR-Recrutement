using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using HR_Rerutement.Migrations;


namespace HR_Rerutement.Models
{
    public class ProjectContext : DbContext
    {
        public ProjectContext() : base("DevRHProject")
        {
           //Database.SetInitializer(new CreateDatabaseIfNotExists<ProjectContext>());
           Database.SetInitializer(new MigrateDatabaseToLatestVersion<ProjectContext, Configuration>());
        }
        public DbSet<Demande> Demandes { get; set; }
        public DbSet<Demandeur> Demandeurs { get; set; }
        public DbSet<Formation> Formations { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Recrutement> Recrutements { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Config> Config { get; set; }
        public DbSet<Message> Message { get; set; }








    }
}