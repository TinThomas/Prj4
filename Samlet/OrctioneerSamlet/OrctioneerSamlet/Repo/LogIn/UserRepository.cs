using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using OrctioneerSamlet.Interfaces.Login;
using OrctioneerSamlet.Models.Login;
using VareDatabase.DBContext;

namespace VareDatabase.Repo
{
    public class UserRepository : Repository<UserEntity>, IUserRepository
    {
        private WalletContext db;
        private UserModelContext _users;

        public UserRepository(WalletContext _db, UserModelContext users) : base(_db)
        {
            db = _db;
            _users = users;
        }

        public async Task<UserEntity> getThisUser(string id)
        {
            var user = await (from i in db.Users
                where i.userID == id
                select i).FirstOrDefaultAsync();
            return user;

        }

        public async Task<UserEntity> getUser(string name)
        {
            var user = await (from i in _users.Users
                where i.Username == name
                select i.UserId).FirstOrDefaultAsync();
            return await (from i in db.Users
                where i.userID == user
                select i).FirstOrDefaultAsync();

        }

        public async Task<int> addUser(UserEntity user)
        {
            db.Add(user);
            return await db.SaveChangesAsync();
        }

        public async Task<int> updateUser(UserEntity user)
        {
            var _user = (from i in db.Users
                where i.userID == user.userID
                select i).FirstOrDefault();
            _user.FirstName = user.FirstName;
            _user.LastName = user.LastName;
            db.Update(_user);
            return await db.SaveChangesAsync();
        }

        public async Task<int> DeleteUser(string id)
        {
            var user = (from i in db.Users
                where i.userID == id
                select i).FirstOrDefault();
            db.Remove(user);
            return await db.SaveChangesAsync();
        }
    }
}