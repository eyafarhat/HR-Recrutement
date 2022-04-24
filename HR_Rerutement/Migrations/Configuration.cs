namespace HR_Rerutement.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HR_Rerutement.Models.ProjectContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HR_Rerutement.Models.ProjectContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            if (context.Roles.Count() == 0)
            {

                context.Roles.AddOrUpdate(
                  new Models.Role { Id_Role = 1, Role1 = "Admin", desc = "Admin" },
                  new Models.Role { Id_Role = 2, Role1 = "Chef", desc = "Chef" },
                  new Models.Role { Id_Role = 3, Role1 = "Demandeur", desc = "Demandeur" },
                  new Models.Role { Id_Role = 4, Role1 = "RH", desc = "RH" });
            }
            if (context.Permissions.Count() == 0)
            {
                var demandeur = context.Demandeurs.Find("390000"); 
                 var role = context.Roles.Find(1);
                demandeur.Permission = new Models.Permission { Id_compte = 2, Empl_Matricule = "390000", Role = role, Password = "123123" };
                context.SaveChanges();




            }




        }
    }
}
