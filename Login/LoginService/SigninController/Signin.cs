using System.Linq;
using SignupController;
using SignupController.Models;
using SQLitePCL;


namespace SigninController
{
    public class Signin
    {
        private UserName username;
        private Password pw;
        public Signin()
        {
            username = new UserName();
            pw = new Password();
        }

        public bool validateUsername(string b)
        {
            if (string.Compare(b, "Hello") == 0)
            {
                return true;
            }
            else return false;
        }

        public int FindUserByName(UserNContext context, string userName)
        {
            var user = context.UserNs.Single(p => p.UserName.Equals(userName));

            return user.UserNId;
        }

        public int FindUserByEmail(UserNContext context, string email)
        {
            var user = context.UserNs.Single(p => p.Email.Equals(email));

            return user.UserNId;
        }
    }
}