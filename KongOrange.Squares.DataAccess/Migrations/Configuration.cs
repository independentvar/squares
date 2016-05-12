using KongOrange.Squares.DomainClasses;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace KongOrange.Squares.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<KongOrange.Squares.DataAccess.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(KongOrange.Squares.DataAccess.ApplicationDbContext context)
        {
            SeedUsers(context);
        }

        private static void SeedUsers(ApplicationDbContext contex)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(contex));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(contex));

            var marty = new ApplicationUser()
            {
                UserName = "Admin",
                Email = "admin@email.com",
                EmailConfirmed = true
            };

            manager.Create(marty, "bobcat_01");

            var julie = new ApplicationUser()
            {
                UserName = "Julie",
                Email = "julie@email.com",
                EmailConfirmed = true
            };

            manager.Create(julie, "bobcat_01");

            var peter = new ApplicationUser()
            {
                UserName = "Peter",
                Email = "peter@email.com",
                EmailConfirmed = true
            };

            manager.Create(peter, "bobcat_01");

            if (roleManager.Roles.Count() == 0)
            {
                roleManager.Create(new IdentityRole {Name = "Admin"});
                roleManager.Create(new IdentityRole {Name = "User"});
            }

            var admin = manager.FindByName("Admin");
            var userJulie = manager.FindByName("Julie");
            var userPeter = manager.FindByName("Peter");

            manager.AddToRoles(admin.Id, new string[] {"Admin", "User"});
            manager.AddToRoles(userJulie.Id, new string[] {"User"});
            manager.AddToRoles(userPeter.Id, new string[] {"User"});
        }
    }
}
