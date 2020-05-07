using Login.SigninControl.Interfaces;

namespace Login.SigninControl.Modules
{
    public class SigninControl : ISigninControl
    {
        private IUserName username;
        private IPassword pw;

        public SigninControl(IUserName user, IPassword pass)
        {
            username = user;
            pw = pass;
        }

        public int validateUsername(string msg)
        {
            int userId = username.getUserId(msg);
            if (userId != -1)
                return userId;
            return -1;
        }

        public bool validatePW(int id, string msg)
        {
            return pw.validatePW(id, msg);
        }
    }
}