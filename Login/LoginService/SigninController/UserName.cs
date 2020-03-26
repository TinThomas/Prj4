using System.Linq;
using SignupController;
using SignupController.Models;
using SQLitePCL;


namespace SigninController
{
    public class UserName
    {
        UserNContext _context = new UserNContext();
        public int getUserId(string input)
        {
            int id;
            //some fancy db calls.
            if (isUsername(input))
            { 
                id = FindUserByName(_context, input);
            }
            else if (isEmail(input))
            {
                id = FindUserByEmail(_context, input);
            }
            else
            {
                id = -1;
            }

            return id;
        }

        public bool isUsername(string input)
        {
            if (input.IndexOf('@') == -1)
            {
                return true;
            }
            return false;
        }

        public bool isEmail(string input)
        {
            if (input.IndexOf('@')!=-1)
            { 
                return true;
            }

            return false;
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