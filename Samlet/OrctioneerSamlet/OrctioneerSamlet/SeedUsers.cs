using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrctioneerSamlet.Interfaces.Login;
using VareDatabase.DBContext;
using VareDatabase.Repo;
using OrctioneerSamlet.Models.Login;

namespace OrctioneerSamlet
{
    public class SeedUsers
    {
        public static void seedUsers(UserModelContext user, PassModelContext pass)
        {
            string id1 = Guid.NewGuid().ToString();
            string id2 = Guid.NewGuid().ToString();
            var m = user.Users.FirstOrDefault();
            if (m == null)
            {
                Console.WriteLine("seeding users");

                var users = new List<UsernameEntity>();
                var h = new UsernameEntity()
                {
                    UserId = id1,
                    Username = "Admin",
                    Email = "Admin@Orctioneer.com"
                };
                users.Add(h);
                h = new UsernameEntity()
                {
                    UserId = id2,
                    Username = "TestUser1",
                    Email = "Test@Orctioneer.com"
                };
                users.Add(h);

                user.Users.AddRange(users);
                user.SaveChangesAsync();
                Console.WriteLine("Done seeding users.");
            }

            var p = pass.Passwords.FirstOrDefault();
            if (p == null)
            {

                var myPasswords = new List<PasswordEntity>();
                var my = new PasswordEntity()
                {
                    UserId = id1,
                    Password = "Password1"
                };
                myPasswords.Add(my);
                my = new PasswordEntity()
                {
                    UserId = id2,
                    Password = "TestPass1"
                };
                myPasswords.Add(my);

                pass.Passwords.AddRange(my);
                pass.SaveChangesAsync();
                Console.WriteLine("done seeding");
            }
        }

    }
}

