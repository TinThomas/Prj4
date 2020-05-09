using System.Threading.Tasks;
using OrctioneerSamlet.Models.Login;
using VareDatabase;

namespace OrctioneerSamlet.Interfaces.Login
{
    public interface IPasswordRepository : IRepository<PasswordEntity>
    {
        Task<bool> validatePassword(PasswordEntity password);
        Task<int> CreatePassword(PasswordEntity password);
        Task<int> updatePassword(PasswordEntity password);
        Task<int> DeletePassword(string id);
    }
}