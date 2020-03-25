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

        public bool validatePassword(string a)
        {
            if (string.Compare(a, "world") == 0)
            {
                return true;
            }
            else return false;
        }
    }
}