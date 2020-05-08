using VareDatabase;
using VareDatabase.Repo;

namespace OrctioneerSamlet.Interfaces.Login
{
    public interface IUserEntity : IRepository<UserRepository>
    {
        void getThisUser(string id);
        void getUser(string name);
        void addUser(UserRepository user);
        void updateUser(UserRepository user);
        void DeleteUser(string id);
    }
}