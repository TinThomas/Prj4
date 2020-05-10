using OrctioneerSamlet.Models.Login;
using VareDatabase;
using VareDatabase.Repo;

namespace OrctioneerSamlet.Interfaces.Login
{
    public interface IUserRepository : IRepository<UserRepository>
    {
        UserEntity getThisUser(string id);
        void getUser(string name);
        void addUser(UserRepository user);
        void updateUser(UserRepository user);
        void DeleteUser(string id);
    }
}