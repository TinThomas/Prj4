namespace SigninController
{
    public class Signin
    {
        private UserName username;
        private Password pw;
        private int userId;
        public Signin()
        {
            username = new UserName();
            pw = new Password();
        }

        public bool validateUsername(string b)
        {
            userId = username.getUserId(b);
            if (userId != -1)
                return true;
            return false;
        }

        public bool validatePW(string b)
        {
            bool check = pw.validatePW(b);
            return check;
        }
    }
}