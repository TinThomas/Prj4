using System.Threading.Tasks;
using OrctioneerSamlet.Models.Login;
using VareDatabase;
using VareDatabase.Repo;

namespace OrctioneerSamlet.Interfaces.Login
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        Task<UserEntity> getThisUser(string id);
        Task<UserEntity> getUser(string name);
        Task<int> addUser(UserEntity user);
        Task<int> updateUser(UserEntity user);
        Task<int> DeleteUser(string id);
    }
}