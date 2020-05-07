using Login.Repos.UserRepo;
using Login.SigninControl.Interfaces;

namespace Login.SigninControl.Modules
{
    public class UserName : IUserName
    {
        //UserNContext _context = new UserNContext();
        private UserRepo _users;

        public UserName()
        {
            _users = UserRepo.GetInstance();
        }

        public int getUserId(string input)
        {
            int id;
            //some fancy db calls.
            if (isUsername(input))
            {
                id = _users.GetUserIdByName(input);
                //id = FindUserByName(_context, input);
            }
            else
            {
                id = _users.GetUserIdByEmail(input);
                //id = FindUserByEmail(_context, input);
            }

            return id;
        }

        public bool isUsername(string input)
        {
            if (input.Contains("@") && input.Contains("."))
            {
                return false;
            }

            return true;
        }

       /* public int FindUserByName(UserNContext context, string userName)
        {
            var user = context.UserNs.Single(p => p.UserName.Equals(userName));

            return user.UserNId;
        }

        public int FindUserByEmail(UserNContext context, string email)
        {
            var user = context.UserNs.Single(p => p.Email.Equals(email));

            return user.UserNId;
        }*/
    }
}