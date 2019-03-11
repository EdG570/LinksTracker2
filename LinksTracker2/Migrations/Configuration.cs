namespace LinksTracker2.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<LinksTracker2.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "LinksTracker2.Models.ApplicationDbContext";
        }

        protected override void Seed(LinksTracker2.Models.ApplicationDbContext context)
        {
            // USERS
            //var store = new UserStore<ApplicationUser>(context);
            //var manager = new UserManager<ApplicationUser>(store);
            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            //roleManager.Create(new IdentityRole("Admin"));

            //// Admin User
            //var admin = new ApplicationUser { UserName = "cole570@hotmail.com", Email = "cole570@hotmail.com" };
            //manager.Create(admin, "Pass123!");
            //manager.AddToRole(admin.Id, "Admin");

            //// Non-Admin User
            //var user = new ApplicationUser { UserName = "email@email.com", Email = "email@email.com" };
            //manager.Create(user, "Pass123!");

            ////// COURSES
            //var courses = new List<Course>
            //{
            //    new Course
            //    {
            //        Id = 1,
            //        Name = "Morgan Hills",
            //        CreatedAt = DateTime.Now,
            //        CreatedBy = "cole570",
            //        TotalHoles = 9,
            //        Par = 72,
            //        Rating = 1,
            //        Slope = 2
            //    }
            //};

            //courses.ForEach(c => context.Courses.AddOrUpdate(c));
            //context.SaveChanges();


            ////// HOLES
            //var holes = new List<Hole>
            //{
            //    new Hole { CourseId = 1, Number = 1, Par = 3, Yardage = 217, CreatedAt = DateTime.Now.AddDays(1) },
            //    new Hole { CourseId = 1, Number = 2, Par = 4, Yardage = 253, CreatedAt = DateTime.Now.AddDays(1) },
            //    new Hole { CourseId = 1, Number = 3, Par = 4, Yardage = 275, CreatedAt = DateTime.Now.AddDays(1) },
            //    new Hole { CourseId = 1, Number = 4, Par = 3, Yardage = 187, CreatedAt = DateTime.Now.AddDays(1) },
            //    new Hole { CourseId = 1, Number = 5, Par = 5, Yardage = 347, CreatedAt = DateTime.Now.AddDays(1) },
            //    new Hole { CourseId = 1, Number = 6, Par = 4, Yardage = 266, CreatedAt = DateTime.Now.AddDays(1) },
            //    new Hole { CourseId = 1, Number = 7, Par = 3, Yardage = 154, CreatedAt = DateTime.Now.AddDays(1) },
            //    new Hole { CourseId = 1, Number = 8, Par = 4, Yardage = 314, CreatedAt = DateTime.Now.AddDays(1) },
            //    new Hole { CourseId = 1, Number = 9, Par = 5, Yardage = 443, CreatedAt = DateTime.Now.AddDays(1) }
            //};

            //holes.ForEach(h => context.Holes.AddOrUpdate(h));
            //context.SaveChanges();

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
        }
    }
}
