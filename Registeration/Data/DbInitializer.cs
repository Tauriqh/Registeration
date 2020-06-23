using Registeration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registeration.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationUserContext context)
        {
            context.Database.EnsureCreated();

            // look for any users
            if (context.ApplicationUser.Any())

            {
                return; // DB has been seeded
            }

            var ApplicationUsers = new ApplicationUser[]
            {
                new ApplicationUser{EmailAddress="admin@bitcube.com", FirstName="Admin", LastName="Admin", Password="admin@"},
                new ApplicationUser{EmailAddress="tauriqh786@gmail.com", FirstName="Tauriq", LastName="Hendricks", Password="132456"},
                new ApplicationUser{EmailAddress="senshi@gmail.com", FirstName="Senshi", LastName="Black", Password="1234@@"}
            };

            foreach (ApplicationUser u in ApplicationUsers)
            {
                context.Add(u);
            }
            context.SaveChanges();
        }
    }
}
