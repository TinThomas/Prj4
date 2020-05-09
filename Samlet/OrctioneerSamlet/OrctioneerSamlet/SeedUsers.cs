using System;
using System.Collections.Generic;
using System.Linq;
using VareDatabase.DBContext;
using OrctioneerSamlet.Models.Login;

namespace OrctioneerSamlet
{
    public class SeedUsers
    {
        public static void seedUsers(UserModelContext user, PassModelContext pass, WalletContext wallet)
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

            var w = wallet.Users.FirstOrDefault();
            if (w == null)
            {
                var u = new List<UserEntity>();
                var _u = new UserEntity()
                {
                    Address = new AddressEntity()
                    {
                        Address = "Randomstreet 200",
                        City = "Hell",
                        Contry = "Yep",
                        Zipcode = 5000
                    },
                    FirstName = "Svend",
                    LastName = "Hansen",
                    userID = id1,
                    wallet = new WalletEntity()
                    {
                        userID = id1,
                        Amount = 5000,
                        card = new CardEntity()
                        {
                            CardNumber = 4230420401,
                            CVVnumber = 511,
                            ExpireYear = 32,
                            ExpireMonth = 06
                        }
                    }

                };
                u.Add(_u);
                _u = new UserEntity()
                {
                    Address = new AddressEntity()
                    {
                        Address = "CoolStreet",
                        City = "This town",
                        Contry = "dk",
                        Zipcode = 8220
                    },
                    FirstName = "Hans",
                    LastName = "Petersen",
                    userID = id1,
                    wallet = new WalletEntity()
                    {
                        userID = id1,
                        Amount = 10000,
                        card = new CardEntity()
                        {
                            CardNumber = 42324242,
                            CVVnumber = 342,
                            ExpireYear = 22,
                            ExpireMonth = 04
                        }
                    }

                };
                u.Add(_u);
                var wait = wallet.AddRangeAsync(_u);
                wallet.SaveChangesAsync();

            }
        }

    }
}

