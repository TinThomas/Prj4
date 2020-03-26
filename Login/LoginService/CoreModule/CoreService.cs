

using SigninController;
using SignupController;

namespace CoreModule
{
    public class CoreService
    {
        private Signin _signin;
        private Signup _signup;
        private string message;
        public CoreService()
        {
            _signin = new Signin();
            _signup = new Signup();
        }

        public bool validateUsername(string b)
        {
            bool check = _signin.validateUsername(b);
            return check;
        }

        public bool validatePassword(string b)
        {
            bool check = _signin.validatePW(b);
            return check;
        }
    }
}