using Login.SigninControl.Modules;
using OrctioneerSamlet.Models.Login;
using VareDatabase;

namespace OrctioneerSamlet.Interfaces.Login
{
    public interface IPasswordRepository : IRepository<PasswordEntity>
    {
        bool validatePassword(PasswordEntity password);
        void CreatePassword(PasswordEntity password);
        void updatePassword(PasswordEntity password);
        void DeletePassword(string id);
    }
}