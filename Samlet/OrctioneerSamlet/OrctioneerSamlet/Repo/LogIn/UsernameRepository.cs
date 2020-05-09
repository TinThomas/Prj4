using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using OrctioneerSamlet.Interfaces.Login;
using OrctioneerSamlet.Models.Login;
using VareDatabase.DBContext;

namespace VareDatabase.Repo
{
    public class UsernameRepository : Repository<UsernameEntity>, IUsernameRepository
    {
        private UserModelContext db;

        public UsernameRepository(UserModelContext _db) : base(_db)
        {
            db = _db;
        }

        public async Task<string> addUser(UsernameEntity user)
        {
            string id = Guid.NewGuid().ToString();
            user.UserId = id;
            var wait = await db.AddAsync(user);
            var done = await db.SaveChangesAsync();
            return id;

        }


        public async Task<string> validateUsername(string username)
        {
            string query = await (from i in db.Users
                where i.Username == username
                select i.UserId).FirstOrDefaultAsync();
            return query;
        }

        public async Task<string> validateEmail(string email)
        {
            string query = await (from i in db.Users
                where i.Email == email
                select i.UserId).FirstOrDefaultAsync();
            return query;
        }

        public async Task<int> updateUsername(UsernameEntity user)
        {
           var query =  (from i in db.Users
                where i.UserId == user.UserId
                      select i).FirstOrDefault();
           query.Username = user.Username;
           db.Update(query);
           return await db.SaveChangesAsync();
        }

        public async Task<int> updateEmail(UsernameEntity user)
        {
            var query = (from i in db.Users
                where i.UserId == user.UserId
                select i).FirstOrDefault();
            query.Email = user.Email;
            db.Update(query);
            return await db.SaveChangesAsync();
        }

        public async Task<bool> CheckUser(UsernameEntity user)
        {
            var query = await (from i in db.Users
                where i.Username == user.Username
                select i).FirstOrDefaultAsync();
            if (query != null)
            {
                return false;
            }
            query = await (from i in db.Users
                where i.Email == user.Email
                select i).FirstOrDefaultAsync();
            if (query != null)
            { 
                return false;
            }

            return true;
        }

        public async Task<int> DeleteUser(string id)
        {
            var query = (from i in db.Users
                where i.UserId == id
                select i).FirstOrDefault();
            db.Remove(query);
            return await db.SaveChangesAsync();
        }

    }
}