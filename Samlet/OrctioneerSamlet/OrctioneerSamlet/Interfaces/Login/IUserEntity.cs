using VareDatabase;
using VareDatabase.Repo;

namespace OrctioneerSamlet.Interfaces.Login
{
    public interface IUserEntity : IRepository<UserEntity>
    {
        void getThisUser(string id);
        void getUser(string name);
        void addUser(UserEntity user);
        void updateUser(UserEntity user);
        void DeleteUser(string id);
    }
}