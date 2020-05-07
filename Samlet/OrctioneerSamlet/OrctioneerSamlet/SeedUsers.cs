using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrctioneerSamlet.Interfaces.Login;
using VareDatabase.DBContext;
using VareDatabase.Repo;
using OrctioneerSamlet.Models.Login;

namespace OrctioneerSamlet
{
    public class SeedUsers
    {
        private readonly IUsernameRepository _user;
        private readonly IPasswordRepository _pass;
        public SeedUsers(UserModelContext user, PassModelContext pass)
        {
            _user = new UsernameRepository(user);
            _pass = new PasswordRepository(pass);
        }

        public void seedUsers()
        {
            var m = _user.GetAll();
            if (m == null)
            {
                var users = new List<UsernameEntity>();
                var h = new UsernameEntity()
                {
                    Username = "Admin",
                    Email = "Admin@Orctioneer.com"
                };
                users.Add(h);
                h = new UsernameEntity()
                {
                    Username = "TestUser1",
                    Email = "Test@Orctioneer.com"
                };
                users.Add(h);
                foreach (var i in users)
                {
                    _user.addUser(i);
                }

                m = _user.GetAll();

                users = new List<UsernameEntity>();
                foreach (var i in m)
                {
                 users.Add(i);   
                }

                    var myPass = new PasswordEntity();
                    myPass.UserId = users[0].UserId;
                    myPass.Password = "Password1";
                    _pass.CreatePassword(myPass);
                    myPass = new PasswordEntity();
                    myPass.UserId = users[1].UserId;
                    myPass.Password = "TestPass1";
                    _pass.CreatePassword(myPass);

            }
        }
    }
}
