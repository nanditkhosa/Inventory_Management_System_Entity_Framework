using Core.Domains;
using Core.Domains;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Data.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<Data.AppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Data.AppContext context)
        {

            var users = context.Set<User>();

            if (users.Any())
                return;

            User adminUser = new User()
            {
                FirstName = "admin",
                LastName = "admin",
                UserName = "admin",
                PasswordHash = "admin",
                IsActive = true,
                LastModifiedUser = "seedMethod",
                LastModifiedTimeStamp = new System.DateTime(),
                CreatedTimeStamp = new System.DateTime(),
                Role = "admin"
            };

            context.Users.Add(adminUser);
            context.SaveChanges();
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
