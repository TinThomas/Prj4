using System;
using OrctioneerSamlet.Models.Login;
using VareDatabase;

namespace OrctioneerSamlet.Interfaces.Login
{
    public interface IUsernameRepository : IRepository<UsernameEntity>
    {
        void addUser(UsernameEntity user);
        string validateUsername(string username);
        string validateEmail(string email);
        void updateUsername(UsernameEntity user);
        void updateEmail(UsernameEntity user);
        bool CheckUser(UsernameEntity user);
        void DeleteUser(string id);

    }
}