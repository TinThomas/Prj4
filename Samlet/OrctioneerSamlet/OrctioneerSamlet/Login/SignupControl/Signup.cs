using System;
using System.Text.Json;
using Login.Repos.PassRepo;
using Login.Repos.UserRepo;

namespace Login.SignupControl
{
    public class Signup : ISignup
    {
        private UserRepo myUsers;
        private PassRepo myPass;
        private int Userid;

        public Signup()
        {
            myUsers = UserRepo.GetInstance();
            myPass = PassRepo.GetInstance();
        }

        public string ConvertUserdata(string msg)
        {
            userData temp = JsonSerializer.Deserialize<userData>(msg);
            int userid = setUsername(temp.userName, temp.email);
            setPassword(userid, temp.password);
            return "ok";

        }

        private int setUsername(string username, string email)
        {
            UserModel myModel = new UserModel();
            myModel.userName = username;
            myModel.Email = email;
            myModel.userID = 0;
            myUsers.AddUser(myModel);
            return Userid = myUsers.GetUserIdByName(username);
        }

        private void setPassword(int userId,string password)
        {
            Console.WriteLine("userid" + userId);
            PassModel myModel = new PassModel();
            myModel.Password = password;
            myModel.userId = userId;
            myPass.AddPassword(myModel);
        }
    }

}