using System;
using OrctioneerSamlet.Models.Login;
using VareDatabase;

namespace OrctioneerSamlet.Interfaces.Login
{
    public interface IUsernameRepository
    {
        void addUser(string username,string email);
        string validateUsername(string username);
        string validateEmail(string email);
        void updateUsername(string id,string username);
        void updateEmail(string id,string email);
        bool CheckUser(string username, string email);
        void DeleteUser(string id);

    }
}