namespace Hack.JourneyWeb.Migrations
{
    using Database.Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Hack.JourneyWeb.Database.JourneyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Hack.JourneyWeb.Database.JourneyContext context)
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

            context.Users.AddOrUpdate(
              p => p.Email,
                new User { Email = "toto@gmail.com", Country = "Australia", Password = "123456" },
                new User { Email = "toto2@gmail.com", Country = "France", Password = "123456" }
            );

            context.SaveChanges();

            var id = context.Users.FirstOrDefault().Id;
            var id2 = context.Users.ToList()[1].Id;

            context.Badges.AddOrUpdate(
                p => p.Id,
                new Badge { Id = 1, TypeBadge = TypeBadge.Romantic, Points = 10, UserId = id },
                new Badge { Id = 2, TypeBadge = TypeBadge.Voyager, Points = 20, UserId = id2 }
            );

            context.Entries.AddOrUpdate(
                p => p.Id,
                new Entry { Id = 1, DateCreated = DateTime.Now, Comments = "Belvédère", UserId = id },
                 new Entry { Id = 1, DateCreated = DateTime.Now, Comments = "Front de mer", UserId = id },
                  new Entry { Id = 1, DateCreated = DateTime.Now, Comments = "Tahiti Tourism", UserId = id },
                   new Entry { Id = 1, DateCreated = DateTime.Now, Comments = "Marché", UserId = id },
                    new Entry { Id = 1, DateCreated = DateTime.Now, Comments = "Museums", UserId = id },
                new Entry { Id = 2, DateCreated = DateTime.Now, Comments = "I like here", UserId = id2 }
            );

            context.SaveChanges();
        }
    }
}
