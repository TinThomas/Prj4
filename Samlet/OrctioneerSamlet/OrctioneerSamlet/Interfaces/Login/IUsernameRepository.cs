using System;
using System.Threading.Tasks;
using OrctioneerSamlet.Models.Login;
using VareDatabase;

namespace OrctioneerSamlet.Interfaces.Login
{
    public interface IUsernameRepository : IRepository<UsernameEntity>
    {
        Task<string> addUser(UsernameEntity user);
        Task<string> validateUsername(string username);
        Task<string> validateEmail(string email);
        void updateUsername(UsernameEntity user);
        void updateEmail(UsernameEntity user);
        Task<bool> CheckUser(UsernameEntity user);
        void DeleteUser(string id);

    }
}