using System.Text.Json;

namespace SignupController
{
    public class Signup
    {
        private userData _userData;

        public void getUserdata(string msg)
        {
            userData temp = JsonSerializer.Deserialize<userData>(msg);
            _userData = new userData();
            _userData.userName = temp.userName;
            _userData.email = temp.email;
            _userData.password = temp.password;
        }

        public int setUsername()
        {
            //DB calls.
            return 42;
        }

        public bool setPassword(int userId)
        {
            //db calls @userId
            return true;
        }
    }

    public class userData
    {
        public string userName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }

}